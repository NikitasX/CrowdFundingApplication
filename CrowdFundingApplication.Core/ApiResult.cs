using System;
using CrowdFundingApplication.Core.Model;

namespace CrowdFundingApplication.Core
{
    public class ApiResult<T>
    {
        public StatusCode ErrorCode { get; set; }

        public string ErrorText { get; set; }

        public T Data { get; set; }

        public bool Success => ErrorCode == StatusCode.Ok;


        public ApiResult()
        { }

        public static ApiResult<T> CreateSuccess(T data)
        {
            return new ApiResult<T>()
            {
                ErrorCode = StatusCode.Ok,
                Data = data
            };
        }

        public ApiResult<U> ToResult<U>()
        {
            var res = new ApiResult<U>()
            {
                ErrorCode = ErrorCode,
                ErrorText = ErrorText
            };

            return res;
        }

        public ApiResult(StatusCode errorCode, string errorText)
        {
            ErrorCode = errorCode;
            ErrorText = errorText;
        }

        internal static ApiResult<User> CreateSuccess()
        {
            throw new NotImplementedException();
        }
    }
}
