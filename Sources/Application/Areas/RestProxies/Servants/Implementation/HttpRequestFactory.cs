using System;
using System.Net.Http;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants.Implementation
{
    internal class HttpRequestFactory : IHttpRequestFactory
    {
        public HttpRequestMessage Create(RestCall restCall)
        {
            var httpRequestMessage = new HttpRequestMessage(MapHttpMethod(restCall.MethodType), restCall.AbsoluteUri);

            CheckAddBody(httpRequestMessage, restCall.Body);
            restCall.Headers.Entries.ForEach(header => httpRequestMessage.Headers.Add(header.Name, header.Value));
            restCall.Security.ApplySecurity(httpRequestMessage);

            return httpRequestMessage;
        }

        private static void CheckAddBody(HttpRequestMessage httpRequestMessage, Maybe<RestCallBody> body)
        {
            body.Evaluate(
                bodyObj =>
                {
                    httpRequestMessage.Content = bodyObj.CreateHttpContent();
                });
        }

        private static HttpMethod MapHttpMethod(RestCallMethodType methodType)
        {
            switch (methodType)
            {
                case RestCallMethodType.Get:
                    return HttpMethod.Get;
                case RestCallMethodType.Post:
                    return HttpMethod.Post;
                case RestCallMethodType.Patch:
                    return new HttpMethod("PATCH");
                case RestCallMethodType.Delete:
                    return HttpMethod.Delete;
                case RestCallMethodType.Put:
                    return HttpMethod.Put;
                default:
                    throw new ArgumentException($"Invalid RestCallMethodType{methodType}.");
            }
        }
    }
}