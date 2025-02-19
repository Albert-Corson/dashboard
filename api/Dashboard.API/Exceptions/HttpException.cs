using System;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Dashboard.API.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public HttpException(HttpStatusCode code)
        {
            StatusCode = code;
        }

        public HttpException(HttpStatusCode code, string message) : base(message)
        {
            StatusCode = code;
        }

        public HttpException(HttpStatusCode code, string message, Exception inner) : base(message, inner)
        {
            StatusCode = code;
        }

        public HttpException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public HttpException(string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        protected HttpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        protected HttpException()
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
