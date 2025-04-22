using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace WixSharp
{
    /// <summary>
    /// An exception thrown when an API call has reached Wix's rate limit.
    /// </summary>
    public class WixRateLimitException : WixException
    {
        public LeakyBucketState LeakyBucket { get; }

        public int? RetryAfterSeconds { get; private set; }

        //When a 429 is returned because the bucket is full, Wix doesn't include the X-Wix-Shop-Api-Call-Limit header in the response
        public WixRateLimitReason Reason => LeakyBucket == null || LeakyBucket.IsFull ? WixRateLimitReason.BucketFull : WixRateLimitReason.Other;

        public WixRateLimitException(HttpResponseMessage response, 
                                         HttpStatusCode httpStatusCode,
                                         IEnumerable<string> errors,
                                         string message,
                                         string jsonError,
                                         string requestId,
                                         LeakyBucketState leakyBucket) 
            : base(response, httpStatusCode, errors, message, jsonError, requestId)
        {
            LeakyBucket = leakyBucket;
            ExtractRetryAfterSeconds(response);
        }

        private void ExtractRetryAfterSeconds(HttpResponseMessage response)
        {
            string strRetryAfer = response.Headers
                                        .FirstOrDefault(kvp => kvp.Key == "Retry-After")
                                        .Value
                                        ?.FirstOrDefault();

            if (int.TryParse(strRetryAfer, out var retryAfterSeconds))
            {
                RetryAfterSeconds = retryAfterSeconds;
            }
        }
    }
}
