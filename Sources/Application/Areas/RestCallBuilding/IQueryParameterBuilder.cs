using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding
{
    public interface IQueryParameterBuilder
    {
        IRestCallBuilder BuildQueryParameters();

        QueryParameters FinishBuilding();

        IQueryParameterBuilder WithQueryParameter(string key, params object[] values);
    }
}