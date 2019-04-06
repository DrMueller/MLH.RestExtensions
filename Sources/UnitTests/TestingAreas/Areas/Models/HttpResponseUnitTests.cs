using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models
{
    [TestFixture]
    public class HttpResponseUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            ConstructorTestBuilderFactory.Constructing<HttpResponse>()
                .UsingDefaultConstructor()
                .WithArgumentValues(true, "Test1")
                .Maps()
                .ToProperty(f => f.IsSuccessStatusCode).WithValue(true)
                .ToProperty(f => f.ResponseBody).WithValue("Test1")
                .BuildMaps()
                .WithArgumentValues(false, null)
                .Maps()
                .ToProperty(f => f.IsSuccessStatusCode).WithValue(false)
                .ToProperty(f => f.ResponseBody).WithValue(null)
                .BuildMaps()
                .Assert();
        }
    }
}