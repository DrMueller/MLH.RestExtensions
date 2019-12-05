using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.RestExtensions.Areas.Exceptions;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants;
using Newtonsoft.Json;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Implementation
{
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated by StructureMap")]
    internal class RestProxy : IRestProxy
    {
        private readonly IHttpClientProxy _httpClientProxy;
        private readonly IHttpRequestFactory _httpRequestFactory;
        private readonly IRestCallBuilderFactory _restCallBuilderFactory;
        private bool _disposed;

        public RestProxy(
            IHttpRequestFactory httpRequestFactory,
            IRestCallBuilderFactory restCallBuilderFactory,
            IHttpClientProxy httpClientProxy)
        {
            _httpRequestFactory = httpRequestFactory;
            _restCallBuilderFactory = restCallBuilderFactory;
            _httpClientProxy = httpClientProxy;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<T> PerformCallAsync<T>(RestCall restCall)
        {
            Guard.ObjectNotNull(() => restCall);
            var request = _httpRequestFactory.Create(restCall);

            var response = await _httpClientProxy.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return ParseResultContent<T>(response.ResponseBody);
            }

            var exceptionMessage = $"Could not get data from {restCall.AbsoluteUri}. Response: {response.ResponseBody}";
            throw new RestCallException(exceptionMessage);
        }

        public async Task<T> PerformCallAsync<T>(Func<IRestCallBuilderFactory, RestCall> restCallBuilderCallback)
        {
            var restCall = restCallBuilderCallback(_restCallBuilderFactory);
            return await PerformCallAsync<T>(restCall);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _httpClientProxy.Dispose();
            }

            _disposed = true;
        }

        private static T ParseResultContent<T>(string content)
        {
            var targetType = typeof(T);
            if (targetType.IsPrimitive || targetType == typeof(string))
            {
                return (T)Convert.ChangeType(content, typeof(T));
            }

            if (string.IsNullOrEmpty(content) || content == "[]")
            {
                return default;
            }

            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        ~RestProxy()
        {
            Dispose(false);
        }
    }
}