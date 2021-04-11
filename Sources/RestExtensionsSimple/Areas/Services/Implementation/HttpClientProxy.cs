using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensionsSimple.Areas.Models;
using Mmu.Mlh.RestExtensionsSimple.Areas.Services.Servants;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Services.Implementation
{
    public sealed class HttpClientProxy : IHttpClientProxy
    {
        private readonly IHttpRequestMessageAdapter _requestMessageAdapter;
        private readonly IHttpCallResultAdapter _resultAdapter;
        private HttpClient _httpClient;

        public HttpClientProxy(
            IHttpCallResultAdapter resultAdapter,
            IHttpRequestMessageAdapter requestMessageAdapter)
        {
            _resultAdapter = resultAdapter;
            _requestMessageAdapter = requestMessageAdapter;
        }

        public async Task<HttpCallResult> SendAsync(HttpCall restCall)
        {
            try
            {
                var requestMessage = _requestMessageAdapter.Adapt(restCall);
                using var httpResponseMessage = await _httpClient.SendAsync(requestMessage);
                await LogResponseAsync(httpResponseMessage, restCall.RequestUri);

                return _resultAdapter.AdaptResult(httpResponseMessage);
            }
            catch (HttpRequestException httpRequestException)
            {
                return HttpCallResult.CreateFailure(httpRequestException.Message);
            }
        }

        public async Task<HttpCallResult<T>> SendAsync<T>(HttpCall restCall)
        {
            try
            {
                var requestMessage = _requestMessageAdapter.Adapt(restCall);
                using var httpResponseMessage = await _httpClient.SendAsync(requestMessage);
                await LogResponseAsync(httpResponseMessage, restCall.RequestUri);

                return await _resultAdapter.AdaptResultAsync<T>(httpResponseMessage);
            }
            catch (HttpRequestException httpRequestException)
            {
                return HttpCallResult<T>.CreateFailure(httpRequestException.Message);
            }
        }

        internal void Initialize(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static async Task LogResponseAsync(HttpResponseMessage httpResponseMessage, string requestUri)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var sb = new StringBuilder();
                sb.Append("HTTP request returned with error. Status Code: ");
                sb.Append(httpResponseMessage.StatusCode.ToString());
                sb.Append(". Request URI: ");
                sb.Append(requestUri);

                if (httpResponseMessage.Content != null)
                {
                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        sb.Append(". Response content: ");
                        sb.AppendLine(responseContent);
                    }
                }

                var message = sb.ToString();
                Debug.WriteLine(message);
            }
        }
    }
}