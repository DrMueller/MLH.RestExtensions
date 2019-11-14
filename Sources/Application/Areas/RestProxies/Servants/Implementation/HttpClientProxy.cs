using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Exceptions;
using Mmu.Mlh.RestExtensions.Areas.Models;

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

        public async Task<HttpResponse> SendAsync(HttpRequestMessage request)
        {
            HttpResponseMessage httpResponseMessage = null;

            try
            {
                httpResponseMessage = await _httpClient.SendAsync(request);
                var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = new HttpResponse(httpResponseMessage.IsSuccessStatusCode, responseBody);
                return result;
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new RestCallException(httpRequestException.Message);
            }
            finally
            {
                httpResponseMessage?.Dispose();
            }
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