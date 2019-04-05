using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models.RestCallBodies;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models.RestCallBodies
{
    [TestFixture]
    public class ApplicationWwwFormUrlEncodedBodyUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            var dict = new Dictionary<string, string>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" },
                { "Key3", "Value3" }
            };

            ConstructorTestBuilderFactory.Constructing<ApplicationWwwFormUrlEncodedBody>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null)
                .Fails()
                .WithArgumentValues(dict)
                .Maps()
                .ToProperty(f => f.Payload).WithValue(dict)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public async Task CreatingHttpContent_MapsDictionaryToString()
        {
            // Arrange
            var dict = new Dictionary<string, string>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" },
                { "Key3", "Value3" }
            };

            const string ExpectedStr = "Key1=Value1&Key2=Value2&Key3=Value3";

            // Act
            var body = new ApplicationWwwFormUrlEncodedBody(dict);
            var actualContent = body.CreateHttpContent();

            // Assert
            Assert.IsTrue(actualContent is StringContent);
            var stringContent = actualContent as StringContent;
            var actualStr = await stringContent.ReadAsStringAsync();
            Assert.AreEqual(ExpectedStr, actualStr);
        }
    }
}