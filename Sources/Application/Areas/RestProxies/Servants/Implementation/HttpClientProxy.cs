using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Infrastructure.Exceptions;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants.Implementation
{
    internal sealed class HttpClientProxy : IHttpClientProxy
    {
        private bool _disposed;
        private HttpClient _httpClient;

        public HttpClientProxy()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<HttpResponse> SendAsync(HttpRequestMessage request)
        {
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                httpResponseMessage = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new RestCallException(httpRequestException.Message);
            }

            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = new HttpResponse(httpResponseMessage.IsSuccessStatusCode, responseBody);

            httpResponseMessage.Dispose();
            return result;
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