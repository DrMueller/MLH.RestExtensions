using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.QueryParamBuilding
{
    public interface IStandaloneQueryParameterBuilder
    {
        QueryParameters Build();
        IStandaloneQueryParameterBuilder WithQueryParameter(string key, params object[] values);
    }
}