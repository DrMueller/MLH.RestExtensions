using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models
{
    [TestFixture]
    public class RestCallResultUnitTests
    {
        [Test]
        public void GenericConstructor_Works()
        {
            var actualObj = new object();

            ConstructorTestBuilderFactory.Constructing<RestCallResult<object>>()
                .UsingDefaultConstructor()
                .WithArgumentValues(200, "Test1", null)
                .Maps()
                .ToProperty(f => f.Content).WithValue(null)
                .BuildMaps()
                .WithArgumentValues(200, "Test1", actualObj)
                .Maps()
                .ToProperty(f => f.Content).WithValue(actualObj)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public void NonGenericConstructor_Works()
        {
            ConstructorTestBuilderFactory.Constructing<RestCallResult>()
                .UsingDefaultConstructor()
                .WithArgumentValues(200, "Test1")
                .Maps()
                .ToProperty(f => f.StatusCode).WithValue(200)
                .ToProperty(f => f.ReturnMessage).WithValue("Test1")
                .BuildMaps()
                .WithArgumentValues(199, null)
                .Maps()
                .ToProperty(f => f.WasSuccess).WithValue(false)
                .BuildMaps()
                .WithArgumentValues(299, null)
                .Maps()
                .ToProperty(f => f.WasSuccess).WithValue(true)
                .BuildMaps()
                .WithArgumentValues(300, null)
                .Maps()
                .ToProperty(f => f.WasSuccess).WithValue(false)
                .BuildMaps()
                .Assert();
        }
    }
}