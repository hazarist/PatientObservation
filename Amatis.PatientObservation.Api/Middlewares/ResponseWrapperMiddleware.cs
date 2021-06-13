using Amatis.PatientObservation.Common.Models.ResponseWrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Amatis.PatientObservation.Api.Middlewares
{
    public class ResponseWrapperMiddleware : ObjectResultExecutor
    {
        public ResponseWrapperMiddleware(OutputFormatterSelector formatterSelector, IHttpResponseStreamWriterFactory writerFactory, ILoggerFactory loggerFactory, IOptions<MvcOptions> mvcOptions) : base(formatterSelector, writerFactory, loggerFactory, mvcOptions)
        {
        }

        public override Task ExecuteAsync(ActionContext context, ObjectResult result)
        {
            var response = new ResponseWrapper();

            if (result.StatusCode == (int)HttpStatusCode.OK 
                || result.StatusCode == (int)HttpStatusCode.Created 
                || result.StatusCode == (int)HttpStatusCode.Accepted
                || result.StatusCode == (int)HttpStatusCode.NoContent)
            {
                response.Data = result.Value;
                response.IsSuccess = true;
                response.Message = "Succesfully completed";
            }
            else
            {
                response.Data = result.Value;
                response.IsSuccess = false;
                response.Message = "Error occurred";
            }

            TypeCode typeCode = Type.GetTypeCode(result.Value.GetType());
            if (typeCode == TypeCode.Object)
                result.Value = response;

            return base.ExecuteAsync(context, result);
        }

    }
}
