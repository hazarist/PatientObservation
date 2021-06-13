using System;
using System.Net;
using System.Runtime.Serialization;

namespace Amatis.PatientObservation.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;
        public string ContentType { get; set; } = @"application/json";

        public NotFoundException()
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
