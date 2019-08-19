using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding;

namespace Mmu.Mlh.RestExtensions.Areas.QueryParamBuilding
{
    public interface IRestQueryParameterBuilder
    {
        IRestCallBuilder BuildQueryParameters();

        IRestQueryParameterBuilder WithQueryParameter(string key, params object[] values);
    }
}