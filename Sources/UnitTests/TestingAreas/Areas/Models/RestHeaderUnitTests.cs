using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models
{
    [TestFixture]
    public class RestHeaderUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            ConstructorTestBuilderFactory.Constructing<RestHeader>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null, "Value1")
                .Fails()
                .WithArgumentValues("Name1", string.Empty)
                .Fails()
                .WithArgumentValues("Name1", "Value1")
                .Maps()
                .ToProperty(f => f.Name).WithValue("Name1")
                .ToProperty(f => f.Value).WithValue("Value1")
                .BuildMaps()
                .Assert();
        }
    }
}