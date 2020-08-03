using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Implementation
{
    internal class RestProxy : IRestProxy
    {
        private readonly IHttpClientProxy _httpClientProxy;
        private readonly IHttpRequestFactory _httpRequestFactory;
        private readonly IRestCallResultAdapter _resultAdapter;

        public RestProxy(
            IHttpRequestFactory httpRequestFactory,
            IRestCallResultAdapter resultAdapter,
            IHttpClientProxy httpClientProxy)
        {
            _httpRequestFactory = httpRequestFactory;
            _resultAdapter = resultAdapter;
            _httpClientProxy = httpClientProxy;
        }

        public async Task<RestCallResult> SendAsync(RestCall restCall)
        {
            try
            {
                var response = await ExecuteCallAsync(restCall);
                var result = _resultAdapter.AdaptResult(response);

                return result;
            }
            catch (HttpRequestException httpRequestException)
            {
                return new RestCallResult(500, httpRequestException.Message);
            }
        }

        public async Task<RestCallResult<T>> SendAsync<T>(RestCall restCall)
        {
            try
            {
                var response = await ExecuteCallAsync(restCall);
                var result = await _resultAdapter.AdaptResultAsync<T>(response);

                return result;
            }
            catch (HttpRequestException httpRequestException)
            {
                return new RestCallResult<T>(500, httpRequestException.Message, default);
            }
        }

        private async Task<HttpResponseMessage> ExecuteCallAsync(RestCall restCall)
        {
            Guard.ObjectNotNull(() => restCall);
            var request = _httpRequestFactory.Create(restCall);

            return await _httpClientProxy.SendAsync(request);
        }
    }
}