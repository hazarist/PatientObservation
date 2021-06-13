using Newtonsoft.Json;

namespace Amatis.PatientObservation.Common.Models.ResponseWrapper
{
    public class ResponseWrapper
    {
        public ResponseWrapper()
        {
            IsSuccess = false;
            Message = null;
            Data = null;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
