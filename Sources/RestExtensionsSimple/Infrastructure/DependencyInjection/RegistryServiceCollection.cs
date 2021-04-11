using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Mmu.Mlh.RestExtensionsSimple.Areas.Services;
using Mmu.Mlh.RestExtensionsSimple.Areas.Services.Implementation;
using Mmu.Mlh.RestExtensionsSimple.Areas.Services.Servants;
using Mmu.Mlh.RestExtensionsSimple.Areas.Services.Servants.Implementation;

namespace Mmu.Mlh.RestExtensionsSimple.Infrastructure.DependencyInjection
{
    public class RegistryServiceCollection : ServiceRegistry
    {
        public RegistryServiceCollection()
        {
            For<IHttpClientProxyFactory>().Use<HttpClientProxyFactory>().Singleton();
            For<IHttpCallResultAdapter>().Use<HttpCallResultAdapter>().Singleton();
            For<IHttpRequestMessageAdapter>().Use<HttpRequestMessageAdapter>().Singleton();

            this.AddHttpClient();
        }
    }
}