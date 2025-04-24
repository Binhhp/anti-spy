using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WixSharp.Infrastructure;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace WixSharp
{
    public abstract class WixService
    {
        public string WixAPIUri { get; set; }

        private static IRequestExecutionPolicy _GlobalExecutionPolicy = new DefaultRequestExecutionPolicy();

        private IRequestExecutionPolicy _ExecutionPolicy;

        private static JsonSerializer _Serializer = Serializer.JsonSerializer;

        private static HttpClient _Client { get; } = new HttpClient();

        protected string _AccessToken { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="WixService" />.
        /// </summary>
        /// <param name="myWixUrl">The shop's *.myWix.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        protected WixService(string shopAccessToken)
        {
            _AccessToken = shopAccessToken;
            WixAPIUri = "https://www.wixapis.com";

            // If there's a global execution policy it should be set as this instance's policy.
            // User can override it with instance-specific execution policy.
            _ExecutionPolicy = _GlobalExecutionPolicy ?? new DefaultRequestExecutionPolicy();
        }
        /// <summary>
        /// Sets the execution policy for this instance only. This policy will always be used over the global execution policy.
        /// The instance will revert back to the global execution policy if you pass null to this method.
        /// </summary>
        public void SetExecutionPolicy(IRequestExecutionPolicy executionPolicy)
        {
            // If the user passes null, revert to the global execution policy.
            _ExecutionPolicy = executionPolicy ?? _GlobalExecutionPolicy ?? new DefaultRequestExecutionPolicy();
        }

        /// <summary>
        /// Sets the global execution policy for all *new* instances. Current instances are unaffected, but you can call .SetExecutionPolicy on them.
        /// </summary>
        public static void SetGlobalExecutionPolicy(IRequestExecutionPolicy globalExecutionPolicy)
        {
            _GlobalExecutionPolicy = globalExecutionPolicy;
        }

        protected RequestUri PrepareRequest(string path)
        {
            var ub = new UriBuilder(WixAPIUri)
            {
                Scheme = "https:",
                Path = path
            };

            return new RequestUri(ub.Uri);
        }

        protected RequestUri PrepareRequestForAppInstance(string path)
        {
            var ub = new UriBuilder(WixAPIUri)
            {
                Scheme = "https:",
                Path = $"apps/v1/{path}"
            };

            return new RequestUri(ub.Uri);
        }

        protected RequestUri PrepareRequestForStores(string path)
        {
            var ub = new UriBuilder(WixAPIUri)
            {
                Scheme = "https:",
                Path = $"stores/v1/{path}"
            };

            return new RequestUri(ub.Uri);
        }
        /// <summary>
        /// Prepares a request to the path and appends the shop's access token header if applicable.
        /// </summary>
        protected CloneableRequestMessage PrepareRequestMessage(RequestUri uri, HttpMethod method, HttpContent content = null)
        {
            var msg = new CloneableRequestMessage(uri.ToUri(), method, content);

            if (!string.IsNullOrEmpty(_AccessToken))
            {
                msg.Headers.Add("Authorization", _AccessToken);
            }

            msg.Headers.Add("Accept", "application/json");

            return msg;
        }

        /// <summary>
        /// Executes a request and returns a JToken for querying. Throws an exception when the response is invalid.
        /// Use this method when the expected response is a single line or simple object that doesn't warrant its own class.
        /// </summary>
        /// <remarks>
        /// This method will automatically dispose the <paramref name="baseClient"/> and <paramref name="content" /> when finished.
        /// </remarks>
        protected async Task<RequestResult<JToken>> ExecuteRequestAsync(RequestUri uri, HttpMethod method,
            CancellationToken cancellationToken, HttpContent content = null)
        {
            using (var baseRequestMessage = PrepareRequestMessage(uri, method, content))
            {
                var policyResult = await _ExecutionPolicy.Run(baseRequestMessage, async (requestMessage) =>
                {
                    var request = _Client.SendAsync(requestMessage, cancellationToken);

                    using (var response = await request)
                    {
                        var rawResult = await response.Content.ReadAsStringAsync();

                        //Check for and throw exception when necessary.
                        CheckResponseExceptions(response, rawResult);

                        JToken jtoken = null;

                        // Don't parse the result when the request was Delete.
                        if (baseRequestMessage.Method != HttpMethod.Delete)
                        {
                            // Make sure that dates are not stripped of any timezone information if tokens are de-serialised into strings/DateTime/DateTimeZoneOffset
                            using (var reader = new JsonTextReader(new StringReader(rawResult)) { DateParseHandling = DateParseHandling.None })
                            {
                                jtoken = await JObject.LoadAsync(reader, cancellationToken);
                            }
                        }

                        return new RequestResult<JToken>(response, jtoken, rawResult);
                    }
                }, cancellationToken);

                return policyResult;
            }
        }

        /// <summary>
        /// Executes a request and returns the given type. Throws an exception when the response is invalid.
        /// Use this method when the expected response is a single line or simple object that doesn't warrant its own class.
        /// </summary>
        /// <remarks>
        /// This method will automatically dispose the <paramref name="baseRequestMessage" /> when finished.
        /// </remarks>
        protected async Task<RequestResult<T>> ExecuteRequestAsync<T>(RequestUri uri, HttpMethod method,
            CancellationToken cancellationToken, HttpContent content = null, string rootElement = null)
        {
            using (var baseRequestMessage = PrepareRequestMessage(uri, method, content))
            {
                var policyResult = await _ExecutionPolicy.Run(baseRequestMessage, async (requestMessage) =>
                {
                    var request = _Client.SendAsync(requestMessage, cancellationToken);

                    using (var response = await request)
                    {
                        var rawResult = await response.Content.ReadAsStringAsync();

                        //Check for and throw exception when necessary.
                        CheckResponseExceptions(response, rawResult);

                        T result = default;
                        if (rootElement != null)
                        {
                            // This method may fail when the method was Delete, which is intendend.
                            // Delete methods should not be parsing the response JSON and should instead
                            // be using the non-generic ExecuteRequestAsync.
                            var reader = new JsonTextReader(new StringReader(rawResult));
                            var data = _Serializer.Deserialize<JObject>(reader).SelectToken(rootElement);
                            result = data.ToObject<T>();
                        }
                        else
                        {
                            var reader = new JsonTextReader(new StringReader(rawResult));
                            var data = _Serializer.Deserialize<JObject>(reader);
                            result = data.ToObject<T>();
                        }

                        return new RequestResult<T>(response, result, rawResult);
                    }
                }, cancellationToken);

                return policyResult;
            }
        }

        private async Task<T> ExecuteWithContentCoreAsync<T>(string path, HttpMethod method, JsonContent content, CancellationToken cancellationToken)
        {
            var req = PrepareRequest(path);
            var response = await ExecuteRequestAsync<T>(req, method, cancellationToken: cancellationToken, content: content);
            return response.Result;
        }

        protected async Task<T> ExecutePostAsync<T>(string path, object jsonContent = null, CancellationToken cancellationToken = default)
        {
            return await ExecuteWithContentCoreAsync<T>(path, HttpMethod.Post, jsonContent == null ? null : new JsonContent(jsonContent), cancellationToken);
        }

        protected async Task<T> ExecutePostAsync<T>(RequestUri req, object jsonContent = null, CancellationToken cancellationToken = default)
        {
            var response = await ExecuteRequestAsync<T>(req, HttpMethod.Post, content: jsonContent == null ? null : new JsonContent(jsonContent), cancellationToken: cancellationToken);
            return response.Result;
        }
        
        protected async Task ExecutePostAsync(string path, object jsonContent = null, CancellationToken cancellationToken = default)
        {
            var req = PrepareRequest(path);
            await ExecuteRequestAsync(req, HttpMethod.Post, cancellationToken: cancellationToken, jsonContent == null ? null : new JsonContent(jsonContent));
        }

        protected async Task<T> ExecutePutAsync<T>(string path, CancellationToken cancellationToken, object jsonContent = null)
        {
            return await ExecuteWithContentCoreAsync<T>(path, HttpMethod.Put, jsonContent == null ? null : new JsonContent(jsonContent), cancellationToken);
        }

        protected async Task ExecuteDeleteAsync(string path, CancellationToken cancellationToken)
        {
            await ExecuteWithContentCoreAsync<JToken>(path, HttpMethod.Delete, null, cancellationToken);
        }

        private async Task<RequestResult<T>> ExecuteGetCoreAsync<T>(string path, Parameterizable queryParams, string fields, CancellationToken cancellationToken)
        {
            var req = PrepareRequest(path);

            if (queryParams != null)
            {
                req.QueryParams.AddRange(queryParams.ToQueryParameters());
            }

            if (!string.IsNullOrEmpty(fields))
            {
                req.QueryParams.Add("fields", fields);
            }

            return await ExecuteRequestAsync<T>(req, HttpMethod.Get, cancellationToken: cancellationToken);
        }

        protected async Task<T> ExecuteGetAsync<T>(string path, string fields, CancellationToken cancellationToken = default)
        {
            return (await ExecuteGetCoreAsync<T>(path, null, fields, cancellationToken)).Result;
        }

        protected async Task<T> ExecuteGetAsync<T>(string path, Parameterizable queryParams = null, CancellationToken cancellationToken = default)
        {
            return (await ExecuteGetCoreAsync<T>(path, queryParams, null, cancellationToken)).Result;
        }
        public async Task ExecuteGetAsync(string path, CancellationToken cancellationToken = default)
        {
            var ub = new UriBuilder(WixAPIUri)
            {
                Scheme = "https:",
                Path = path
            };
            await _Client.SendAsync(new HttpRequestMessage
            {
                RequestUri = ub.Uri,
                Method = HttpMethod.Get,
            });
        }

        //protected async Task<ListResult<T>> ExecuteGetListAsync<T>(string path, ListFilter filter, CancellationToken cancellationToken = default)
        //{
        //    var result = await ExecuteGetCoreAsync<List<T>>(path, filter, null, cancellationToken);
        //    return ParseLinkHeaderToListResult(result);
        //}

        /// <summary>
        /// Checks a response for exceptions or invalid status codes. Throws an exception when necessary.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <<param name="rawResponse">The response body returned by Wix.</param>
        public static void CheckResponseExceptions(HttpResponseMessage response, string rawResponse)
        {
            var statusCode = (int)response.StatusCode;

            // No error if response was between 200 and 300.
            if (statusCode >= 200 && statusCode < 300)
            {
                return;
            }

            var requestIdHeader = response.Headers.FirstOrDefault(h => h.Key.Equals("X-Request-Id", StringComparison.OrdinalIgnoreCase));
            var requestId = requestIdHeader.Value?.FirstOrDefault();
            var code = response.StatusCode;
            var statusMessage = $"{(int)code} {response.ReasonPhrase}";

            // If the error was caused by reaching the API rate limit, throw a rate limit exception.
            if ((int)code == 429 /* Too many requests */)
            {
                string rateExceptionMessage;
                IEnumerable<string> errors;
                
                if (TryParseErrorJson(rawResponse, out var rateLimitErrors))
                {
                    rateExceptionMessage = $"({statusMessage}) {rateLimitErrors.First()}";
                    errors = rateLimitErrors;
                }
                else
                {
                    var baseMessage = "Exceeded the rate limit for api client. Reduce request rates to resume uninterrupted service.";
                    rateExceptionMessage = $"({statusMessage}) {baseMessage}";
                    errors = new List<string>{ baseMessage };
                }

                throw new WixRateLimitException(response, code, errors, rateExceptionMessage, rawResponse, requestId, LeakyBucketState.Get(response));
            }

            var contentType = response.Content.Headers.GetValues("Content-Type").FirstOrDefault();

            if (contentType.StartsWithIgnoreCase("application/json") || contentType.StartsWithIgnoreCase("text/json"))
            {
                IEnumerable<string> errors;
                string exceptionMessage;

                if (TryParseErrorJson(rawResponse, out var parsedErrors))
                {
                    var firstError = parsedErrors.First();
                    var totalErrors = parsedErrors.Count();
                    var baseErrorMessage = $"({statusMessage}) {firstError}";

                    switch (totalErrors)
                    {
                        case 1 :
                            exceptionMessage = baseErrorMessage;
                            break;
                        
                        case 2:
                            exceptionMessage = $"{baseErrorMessage} (and one other error)";
                            break;
                        
                        default:
                            exceptionMessage = $"{baseErrorMessage} (and {totalErrors} other errors)";
                            break;
                    }
                    
                    errors = parsedErrors;
                }
                else
                {
                    exceptionMessage = $"({statusMessage}) Wix returned {statusMessage}, but WixSharp was unable to parse the response JSON.";
                    errors = new List<string>
                    {
                        exceptionMessage
                    };
                }

                throw new WixException(response, code, errors, exceptionMessage, rawResponse, requestId);
            }

            var message = $"({statusMessage}) Wix returned {statusMessage}, but there was no JSON to parse into an error message.";
            var customErrors = new List<string>
            {
                message
            };

            throw new WixException(response, code, customErrors, message, rawResponse, requestId);
        }

        /// <summary>
        /// Attempts to parse a JSON string for Wix API errors. Returns false if the string cannot be parsed or contains no errors. 
        /// </summary>
        public static bool TryParseErrorJson(string json, out List<string> output)
        {
            output = null;
            
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }

            var errors = new List<string>();

            try
            {
                var parsed = JToken.Parse(string.IsNullOrEmpty(json) ? "{}" : json);

                if (parsed.Type != JTokenType.Object)
                {
                    return false;
                }

                // Errors can be any of the following:
                // 1. { "errors": "some error message"}
                // 2. { "errors": { "order" : "some error message" } }
                // 3. { "errors": { "order" : [ "some error message" ] } }
                // 4. { "error": "invalid_request", error_description:"The authorization code was not found or was already used" }
                // 5. { "error": "location_id must be specified when creating fulfillments." }

                if (parsed.Any(p => p.Path == "error") && parsed.Any(p => p.Path == "error_description"))
                {
                    // Error is type #4
                    var description = parsed["error_description"].Value<string>();
                    var errorType = parsed["error"].Value<string>();
                    
                    errors.Add($"{errorType}: {description}");
                }
                else if (parsed.Any(p => p.Path == "error"))
                {
                    // Error is type #5
                    var description = parsed["error"].Value<string>();
                    
                    errors.Add(description);
                }
                else if (parsed.Any(x => x.Path == "errors"))
                {
                    var parsedErrors = parsed["errors"];

                    // errors can be either a single string, or an array of other errors
                    if (parsedErrors.Type == JTokenType.String)
                    {
                        // errors is type #1
                        var description = parsedErrors.Value<string>();

                        errors.Add(description);
                    }
                    else
                    {
                        // errors is type #2 or #3
                        foreach (var val in parsedErrors.Values())
                        {
                            var name = val.Path.Split('.').Last();

                            if (val.Type == JTokenType.String)
                            {
                                var description = val.Value<string>();
                                
                                errors.Add($"{name}: {description}");
                            }
                            else if (val.Type == JTokenType.Array)
                            {
                                foreach (var msg in val.Values<string>())
                                {
                                    errors.Add($"{name}: {msg}");
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (JsonReaderException)
            {
                return false;
            }

            if (!errors.Any())
            {
                return false;
            }

            output = errors;
            
            return true;
        }

        /// <summary>
        /// Parses a link header value into a ListResult<T>. The Items property will need to be manually set. 
        /// </summary>
        //protected ListResult<T> ParseLinkHeaderToListResult<T>(RequestResult<List<T>> requestResult)
        //{
        //    return new ListResult<T>(requestResult.Result, requestResult.RawLinkHeaderValue == null ? null : LinkHeaderParser.Parse<T>(requestResult.RawLinkHeaderValue));
        //}
    }
}
