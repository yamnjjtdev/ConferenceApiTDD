using System;
using System.Net;
using System.Runtime.Serialization;

namespace ConferenceApiTDD.Lib
{
    [Serializable]
    public class ConferenceApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ConferenceApiException() { }

        public ConferenceApiException(HttpStatusCode statusCode)
            => StatusCode = statusCode;

        public ConferenceApiException(HttpStatusCode statusCode, string message) : base(message)
            => StatusCode = statusCode;

        public ConferenceApiException(HttpStatusCode statusCode, string message, Exception inner) : base(message, inner)
            => StatusCode = statusCode;
    }
}
