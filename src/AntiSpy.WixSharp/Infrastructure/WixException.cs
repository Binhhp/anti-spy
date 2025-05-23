﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace WixSharp
{
    public class WixException : Exception
    {
        public HttpResponseMessage HttpResponse { get; }

        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// The XRequestId header returned by Shopify. Can be used when working with the Shopify support team to identify the failed request.
        /// </summary>
        public string RequestId { get; }

        /// <remarks>
        /// List is always initialized to ensure null reference errors won't be thrown when trying to check error messages.
        /// </remarks>
        public IEnumerable<string> Errors { get; } = Enumerable.Empty<string>();

        /// <summary>
        /// The raw JSON string returned by Shopify.
        /// </summary>
        public string RawBody { get; }

        public WixException() { }

        public WixException(string message) : base(message) { }

        public WixException(HttpResponseMessage response, HttpStatusCode httpStatusCode, IEnumerable<string> errors, string message, string rawBody, string requestId) : base(message)
        {
            HttpResponse = response;
            HttpStatusCode = httpStatusCode;
            Errors = (errors ?? Enumerable.Empty<string>()).ToArray();
            RawBody = rawBody;
            RequestId = requestId;
        }
    }
}
