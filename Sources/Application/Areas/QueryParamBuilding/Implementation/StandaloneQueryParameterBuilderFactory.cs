namespace Mmu.Mlh.RestExtensions.Areas.QueryParamBuilding.Implementation
{
    internal class StandaloneQueryParameterBuilderFactory : IStandaloneQueryParameterBuilderFactory
    {
        public IStandaloneQueryParameterBuilder StartBuilding()
        {
            return new StandaloneQueryParameterBuilder();
        }
    }
}