namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding
{
    public interface IQueryParameterBuilder
    {
        IRestCallBuilder Build();

        IQueryParameterBuilder WithQueryParameter(string key, params object[] values);
    }
}