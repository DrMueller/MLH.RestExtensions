using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Mmu.Mlh.RestExtensionsSimple.Areas.Models;
using Newtonsoft.Json;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Services.Servants.Implementation
{
    public class HttpRequestMessageAdapter : IHttpRequestMessageAdapter
    {
        public const string ContentMediaType = "application/json";

        public HttpRequestMessage Adapt(HttpCall restCall)
        {
            var httpMethod = MapHttpMethod(restCall.MethodType);
            var request = new HttpRequestMessage(httpMethod, restCall.RequestUri);
            CheckAddBody(request, restCall.Body);
            ApplyBasicAuth(request, restCall.Credentials);

            return request;
        }

        private static void ApplyBasicAuth(HttpRequestMessage request, BasicAuthCredentials credentials)
        {
            var basicAuthStr = $"{credentials.UserName}:{credentials.Password}";
            var encodedCredentials = Convert.ToBase64String(Encoding.Default.GetBytes(basicAuthStr));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);
        }

        private static void CheckAddBody(HttpRequestMessage requestMessage, object body)
        {
            if (body == null)
            {
                return;
            }

            string jsonBody;

            if (body is string s)
            {
                jsonBody = s;
            }
            else
            {
                jsonBody = JsonConvert.SerializeObject(body);
            }

            requestMessage.Content = new StringContent(jsonBody);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentMediaType);
        }

        private static HttpMethod MapHttpMethod(HttpCallMethodType methodType)
        {
            switch (methodType)
            {
                case HttpCallMethodType.Get:
                    return HttpMethod.Get;
                case HttpCallMethodType.Post:
                    return HttpMethod.Post;
                case HttpCallMethodType.Patch:
                    return new HttpMethod("PATCH");
                case HttpCallMethodType.Delete:
                    return HttpMethod.Delete;
                case HttpCallMethodType.Put:
                    return HttpMethod.Put;
                default:
                    throw new ArgumentException($"Invalid RestCallMethodType{methodType}.");
            }
        }
    }
}