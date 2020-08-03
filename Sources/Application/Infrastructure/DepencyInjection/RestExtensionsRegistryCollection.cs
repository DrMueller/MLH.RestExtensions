using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Mmu.Mlh.RestExtensions.Areas.QueryParamBuilding;
using Mmu.Mlh.RestExtensions.Areas.QueryParamBuilding.Implementation;
using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding;
using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding.Implementation;
using Mmu.Mlh.RestExtensions.Areas.RestProxies;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Implementation;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants.Implementation;

namespace Mmu.Mlh.RestExtensions.Infrastructure.DepencyInjection
{
    public class RestExtensionsRegistryCollection : ServiceRegistry
    {
        public RestExtensionsRegistryCollection()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<RestExtensionsRegistryCollection>();
                    scanner.WithDefaultConventions();
                });

            For<IRestProxy>().Use<RestProxy>().Singleton();
            For<IRestCallBuilderFactory>().Use<RestCallBuilderFactory>().Singleton();
            For<IStandaloneQueryParameterBuilderFactory>().Use<StandaloneQueryParameterBuilderFactory>().Singleton();
            For<IHttpRequestFactory>().Use<HttpRequestFactory>().Singleton();
            For<IRestCallResultAdapter>().Use<RestCallResultAdapter>().Singleton();
            For<IHttpClientProxy>().Use<HttpClientProxy>().Singleton();
            this.AddHttpClient();
        }
    }
}