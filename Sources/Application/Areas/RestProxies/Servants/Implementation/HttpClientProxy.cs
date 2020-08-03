using System.Net.Http;
using System.Threading.Tasks;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants.Implementation
{
    internal sealed class HttpClientProxy : IHttpClientProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                return await httpClient.SendAsync(request);
            }
        }
    }
}