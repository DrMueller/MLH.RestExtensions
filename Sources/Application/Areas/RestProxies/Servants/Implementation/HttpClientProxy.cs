using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants.Implementation
{
    internal sealed class HttpClientProxy : IHttpClientProxy
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;

        public HttpClientProxy()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await _httpClient.SendAsync(request);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _httpClient.Dispose();
            }

            _disposed = true;
        }

        ~HttpClientProxy()
        {
            Dispose(false);
        }
    }
}