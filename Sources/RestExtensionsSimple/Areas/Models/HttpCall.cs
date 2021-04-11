using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Models
{
    public class HttpCall
    {
        public object Body { get; }
        public BasicAuthCredentials Credentials { get; }
        public HttpCallMethodType MethodType { get; }
        public string RequestUri { get; }

        public HttpCall(
            string requestUri,
            HttpCallMethodType methodType,
            BasicAuthCredentials credentials,
            object body = null)
        {
            Guard.StringNotNullOrEmpty(() => requestUri);
            Guard.That(() => body == null || methodType != HttpCallMethodType.Get, "Body in GET request is not allowed");

            RequestUri = requestUri;
            MethodType = methodType;
            Credentials = credentials;
            Body = body;
        }
    }
}