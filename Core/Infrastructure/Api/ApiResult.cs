using System.Net;

namespace TMDb.Core
{
    public class ApiResult
    {
        internal static ApiResult<T> Create<T>(T data, bool success, HttpStatusCode statusCode, string message = "")
            => new ApiResult<T>(data, success, statusCode, message);
    }

    public class ApiResult<T>
    {
        public ApiResult(T data, bool success, HttpStatusCode statusCode, string message = "")
        {
            Data = data;
            Message = message;
            Success = success;
            StatusCode = statusCode;
        }

        public T Data { get; }

        public bool Success { get; }

        public string Message { get; }

        public HttpStatusCode StatusCode { get; }
    }
}
