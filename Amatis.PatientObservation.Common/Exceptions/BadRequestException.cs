using System;
using System.Net;
using System.Runtime.Serialization;

namespace Amatis.PatientObservation.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public string ContentType { get; set; } = @"application/json";

        public BadRequestException()
        {
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
