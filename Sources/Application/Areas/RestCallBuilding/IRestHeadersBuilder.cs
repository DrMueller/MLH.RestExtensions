namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding
{
    public interface IRestHeadersBuilder
    {
        IRestHeadersBuilder AddHeader(string name, string value);

        IRestCallBuilder BuildHeaders();
    }
}