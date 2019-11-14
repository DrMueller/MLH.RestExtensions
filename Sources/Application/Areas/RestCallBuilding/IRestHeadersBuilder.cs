namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding
{
    public interface IRestHeadersBuilder
    {
        IRestCallBuilder BuildHeaders();
        IRestHeadersBuilder WithHeader(string name, string value);
    }
}