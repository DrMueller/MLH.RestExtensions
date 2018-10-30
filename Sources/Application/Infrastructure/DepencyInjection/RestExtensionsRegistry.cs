using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding;
using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding.Implementation;
using Mmu.Mlh.RestExtensions.Areas.RestProxies;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Implementation;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants.Implementation;
using StructureMap;

namespace Mmu.Mlh.RestExtensions.Infrastructure.DepencyInjection
{
    public class RestExtensionsRegistry : Registry
    {
        public RestExtensionsRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<RestExtensionsRegistry>();
                    scanner.WithDefaultConventions();
                });

            For<IRestProxy>().Use<RestProxy>().Singleton();
            For<IRestCallBuilderFactory>().Use<RestCallBuilderFactory>().Singleton();
            For<IHttpRequestFactory>().Use<HttpRequestFactory>().Singleton();
            For<IHttpClientProxy>().Use<HttpClientProxy>().AlwaysUnique();
            For<IHttpClientProxyFactory>().Use<HttpClientProxyFactory>().Singleton();
        }
    }
}