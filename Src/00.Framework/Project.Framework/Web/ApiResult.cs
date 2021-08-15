using Newtonsoft.Json;
using Project.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.Web
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Errors = null;

        public ApiResult(bool isSuccess, StatusCode statusCode, string message = null, List<string> errors = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
            Errors = errors;
        }
    }
    public class ApiResult<TData> : ApiResult where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        public ApiResult(bool isSuccess, StatusCode statusCode, TData data, string message = null) : base(isSuccess, statusCode, message)
        {
            Data = data;
        }
    }
}
