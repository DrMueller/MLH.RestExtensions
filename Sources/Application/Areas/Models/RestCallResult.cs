using System.Diagnostics.CodeAnalysis;

namespace Mmu.Mlh.RestExtensions.Areas.Models
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification =
        "It makes sense to keep these Classes together")]
    public class RestCallResult<T> : RestCallResult
    {
        public T Content { get; }

        public RestCallResult(int statusCode, string returnMessage, T content)
            : base(statusCode, returnMessage)
        {
            Content = content;
        }

        public static RestCallResult<T> CreateGenericFailure()
        {
            return new RestCallResult<T>(500, string.Empty, default);
        }

        public static RestCallResult<T> CreateGenericSuccess(T result)
        {
            return new RestCallResult<T>(200, string.Empty, result);
        }
    }

    public class RestCallResult
    {
        public string ReturnMessage { get; }
        public int StatusCode { get; }
        public bool WasSuccess { get; }

        public RestCallResult(int statusCode, string returnMessage)
        {
            WasSuccess = statusCode >= 200 && statusCode <= 299;
            StatusCode = statusCode;
            ReturnMessage = returnMessage;
        }

        public static RestCallResult CreateGenericSuccess()
        {
            return new RestCallResult(200, string.Empty);
        }
    }
}