using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models
{
    [TestFixture]
    public class QueryParameterUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            ConstructorTestBuilderFactory.Constructing<QueryParameter>()
                .UsingDefaultConstructor()
                .WithArgumentValues("Test", "Test1")
                .Succeeds()
                .WithArgumentValues("Test", "Test1", "Test2", "Test3")
                .Succeeds()
                .WithArgumentValues("Test")
                .Succeeds()
                .WithArgumentValues(null, "Test1")
                .Fails()
                .WithArgumentValues("Test", null)
                .Fails()
                .Assert();
        }
    }
}