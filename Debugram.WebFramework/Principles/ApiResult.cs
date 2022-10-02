using Debugram.Common.Utilities;
using Debugram.CommonModel.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Debugram.WebFramework.Principles
{
    public class ApiResult
    {
        public ApiResult(bool isSuccess, ResultApiStatusCode statusCode, string? message = null)
        {
            IsSuccess = isSuccess;
            Message = message ?? statusCode.ToDisplay();
            StatusCode = statusCode;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ResultApiStatusCode StatusCode { get; set; }

        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ResultApiStatusCode.Success);
        }
        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ResultApiStatusCode.BadRequest);
        }
        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var Message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessage = errors.SelectMany(p => (string[])p.Value).Distinct();
                Message = string.Join(" | ", errorMessage);
            }
            return new ApiResult(false, ResultApiStatusCode.BadRequest, Message);
        }
        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(true, ResultApiStatusCode.Success, result.Content);
        }
        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(false, ResultApiStatusCode.NotFound);
        }
    }
    public class ApiResult<TData> : ApiResult
       where TData : class
    {
        public ApiResult(bool isSuccess, ResultApiStatusCode statusCode, TData data, string message = null)
           : base(isSuccess, statusCode, message)
        {
            Data = data;
        }
        public TData Data { get; set; }

        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, ResultApiStatusCode.Success, data);
        }
        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, ResultApiStatusCode.Success, null);
        }
        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, ResultApiStatusCode.BadRequest, null);
        }
        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>(true, ResultApiStatusCode.Success, (TData)result.Value);
        }
        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var Message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessage = errors.SelectMany(p => (string[])p.Value).Distinct();
                Message = string.Join(" | ", errorMessage);
            }
            return new ApiResult<TData>(false, ResultApiStatusCode.BadRequest, null, Message);
        }
        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(false, ResultApiStatusCode.NotFound, null);
        }
        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(false, ResultApiStatusCode.NotFound, (TData)result.Value);
        }
    }
}
