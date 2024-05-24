using System;
using System.Net;

namespace Microsoft.SCIM
{
    public class CustomHttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public CustomHttpResponseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
