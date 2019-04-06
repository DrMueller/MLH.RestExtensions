using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models.RestCallBodies;
using Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models.RestCallBodies.TestingModels;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models.RestCallBodies
{
    [TestFixture]
    public class ApplicationJsonRestCallBodyUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            var obj = new object();

            ConstructorTestBuilderFactory.Constructing<ApplicationJsonBody>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null)
                .Fails()
                .WithArgumentValues(obj)
                .Maps()
                .ToProperty(f => f.Payload).WithValue(obj)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public async Task CreatingHttpContent_WithPassedJsonString_ReturnStringContentWithJson()
        {
            // Arrange
            var individual = Individual.CreateOne();
            var individualJson = JsonConvert.SerializeObject(individual);

            // Act
            var body = new ApplicationJsonBody(individualJson);
            var actualContent = body.CreateHttpContent();

            // Assert
            Assert.IsTrue(actualContent is StringContent);
            var stringContent = actualContent as StringContent;
            var actualStr = await stringContent.ReadAsStringAsync();
            Assert.AreEqual(actualStr, individualJson);
        }

        [Test]
        public async Task CreatingHttpContent_WithPassedObject_ConvertsAndReturnStringContentWithJson()
        {
            // Arrange
            var individual = Individual.CreateOne();
            var expectedJson = JsonConvert.SerializeObject(individual);

            // Act
            var body = new ApplicationJsonBody(individual);
            var actualContent = body.CreateHttpContent();

            // Assert
            Assert.IsTrue(actualContent is StringContent);
            var stringContent = actualContent as StringContent;
            var actualStr = await stringContent.ReadAsStringAsync();
            Assert.AreEqual(actualStr, expectedJson);
        }
    }
}