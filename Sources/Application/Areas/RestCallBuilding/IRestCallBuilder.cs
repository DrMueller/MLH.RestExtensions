using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;
using Mmu.Mlh.RestExtensions.Areas.QueryParamBuilding;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding
{
    public interface IRestCallBuilder
    {
        RestCall Build();

        IRestCallBuilder WithBody(RestCallBody body);

        IRestHeadersBuilder WithHeaders();

        IRestQueryParameterBuilder WithQueryParameters();

        IRestCallBuilder WithResourcePath(string resourcePath);

        IRestCallBuilder WithSecurity(RestSecurity security);
    }
}