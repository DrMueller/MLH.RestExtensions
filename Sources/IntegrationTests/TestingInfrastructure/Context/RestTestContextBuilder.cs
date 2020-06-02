using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.TestingExtensions.Areas.IntegrationTesting.Contexts.Builders;
using Mmu.Mlh.TestingExtensions.Areas.IntegrationTesting.Contexts.Models;

namespace Mmu.Mlh.RestExtensions.IntegrationTests.TestingInfrastructure.Context
{
    internal static class RestTestContextBuilder
    {
        private static readonly ContainerConfiguration _containerConfig = ContainerConfiguration.CreateFromAssembly(typeof(RestTestContextBuilder).Assembly);

        internal static IIntegrationTestContext Create()
        {
            return IntegrationTestContextBuilderFactory.StartBuilding(_containerConfig).Build();
        }
    }
}