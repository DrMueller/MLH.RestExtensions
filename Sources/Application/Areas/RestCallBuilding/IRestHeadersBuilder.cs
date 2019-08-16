namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding
{
    public interface IRestHeadersBuilder
    {
        IRestHeadersBuilder WithHeader(string name, string value);

        IRestCallBuilder BuildHeaders();
    }
}