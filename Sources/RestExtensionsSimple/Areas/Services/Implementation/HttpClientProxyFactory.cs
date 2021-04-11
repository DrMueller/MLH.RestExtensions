using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Services.Implementation
{
    public class HttpClientProxyFactory : IHttpClientProxyFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _serviceProvider;

        public HttpClientProxyFactory(
            IServiceProvider serviceProvider,
            IHttpClientFactory httpClientFactory)
        {
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
        }

        public IHttpClientProxy Create()
        {
            var proxy = _serviceProvider.GetRequiredService<HttpClientProxy>();
            var httpClient = _httpClientFactory.CreateClient();
            proxy.Initialize(httpClient);

            return proxy;
        }
    }
}