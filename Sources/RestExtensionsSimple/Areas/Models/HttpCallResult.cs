namespace Mmu.Mlh.RestExtensionsSimple.Areas.Models
{
    public class HttpCallResult<T> : HttpCallResult
    {
        public T Result { get; }

        public HttpCallResult(int statusCode, string returnMessage, T result)
            : base(statusCode, returnMessage)
        {
            Result = result;
        }

        public static HttpCallResult<T> CreateFailure(string returnMessage, T result = default)
        {
            return new HttpCallResult<T>(500, returnMessage, result);
        }

        public static HttpCallResult<T> CreateSuccess(T result)
        {
            return new HttpCallResult<T>(200, string.Empty, result);
        }
    }

    public class HttpCallResult
    {
        public string ReturnMessage { get; }
        public int StatusCode { get; }
        public bool WasSuccess => StatusCode >= 200 && StatusCode <= 299;

        public HttpCallResult(int statusCode, string returnMessage)
        {
            StatusCode = statusCode;
            ReturnMessage = returnMessage;
        }

        public static HttpCallResult CreateFailure(string returnMessage)
        {
            return new HttpCallResult(500, returnMessage);
        }

        public static HttpCallResult CreateSuccess()
        {
            return new HttpCallResult(200, string.Empty);
        }
    }
}