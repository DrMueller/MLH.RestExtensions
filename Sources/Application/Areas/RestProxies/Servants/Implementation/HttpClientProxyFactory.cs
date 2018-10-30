using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants.Implementation
{
    internal class HttpClientProxyFactory : IHttpClientProxyFactory
    {
        private readonly IServiceLocator _serviceLocator;

        public HttpClientProxyFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IHttpClientProxy Create()
        {
            return _serviceLocator.GetService<IHttpClientProxy>();
        }
    }
}