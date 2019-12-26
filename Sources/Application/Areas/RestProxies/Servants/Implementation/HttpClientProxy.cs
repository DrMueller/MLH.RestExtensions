using System;
using System.Net.Http;
using System.Text;
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
                var exception = await BuildExceptionAsync(httpRequestException, httpResponseMessage);
                throw exception;
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

        private static async Task<RestCallException> BuildExceptionAsync(HttpRequestException httpRequestException, HttpResponseMessage response)
        {
            var sb = new StringBuilder();
            sb.Append("Message: ");
            sb.AppendLine(httpRequestException.Message);

            if (response != null)
            {
                sb.Append("Status code: ");
                sb.AppendLine(response.StatusCode.ToString());

                if (!string.IsNullOrEmpty(response.ReasonPhrase))
                {
                    sb.Append("Reason: ");
                    sb.AppendLine(response.ReasonPhrase);
                }

                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        sb.Append("Response content: ");
                        sb.AppendLine(responseContent);
                    }
                }
            }

            var exceptionMessage = sb.ToString();
            throw new RestCallException(exceptionMessage);
        }

        ~HttpClientProxy()
        {
            Dispose(false);
        }
    }
}