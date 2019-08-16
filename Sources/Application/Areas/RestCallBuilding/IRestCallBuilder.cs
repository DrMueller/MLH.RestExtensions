using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding
{
    public interface IRestCallBuilder
    {
        RestCall Build();

        IRestCallBuilder WithBody(RestCallBody body);

        IRestHeadersBuilder WithHeaders();

        IQueryParameterBuilder WithQueryParameters();

        IRestCallBuilder WithResourcePath(string resourcePath);

        IRestCallBuilder WithSecurity(RestSecurity security);
    }
}