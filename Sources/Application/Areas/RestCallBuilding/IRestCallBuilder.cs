using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding
{
    public interface IRestCallBuilder
    {
        RestCall Build();

        IRestCallBuilder WithBody(RestCallBody body);

        IRestHeadersBuilder WithHeaders();

        IRestCallBuilder WithQueryParameter(string key, params object[] values);

        IRestCallBuilder WithResourcePath(string resourcePath);

        IRestCallBuilder WithSecurity(RestSecurity security);
    }
}