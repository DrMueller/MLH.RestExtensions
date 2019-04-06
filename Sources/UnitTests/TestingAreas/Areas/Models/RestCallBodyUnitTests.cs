using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.RestCallBodies;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models
{
    [TestFixture]
    public class RestCallBodyUnitTests
    {
        [Test]
        public void CreatingApplicationJson_CreatesApplicationJson()
        {
            // Act
            var actualBody = RestCallBody.CreateApplicationJson("Tra");

            // Assert
            Assert.IsInstanceOf<ApplicationJsonBody>(actualBody);
        }

        [Test]
        public void CreatingApplicationWwwFormUrlEncoded_CreatesApplicationWwwFormUrlEncoded()
        {
            // Act
            var actualBody = RestCallBody.CreateApplicationWwwFormUrlEncoded(new Dictionary<string, string>());

            // Assert
            Assert.IsInstanceOf<ApplicationWwwFormUrlEncodedBody>(actualBody);
        }

        [Test]
        public void CreatingHttpContent_CreatesContentTypeFromSubClass()
        {
            // Arrange
            var actualBody = RestCallBody.CreateApplicationJson("Tra");

            // Act
            var actualHttpContent = actualBody.CreateHttpContent();

            // Assert
            var actualContentType = actualHttpContent.Headers.ContentType;

            Assert.IsNotNull(actualContentType);
            Assert.IsInstanceOf<MediaTypeHeaderValue>(actualContentType);
            actualContentType.MediaType = ApplicationJsonBody.MediaTypeAppJson;
        }

        [Test]
        public async Task CreatingHttpContent_CreatesContentValueFromSubClass()
        {
            // Arrange
            var dict = new Dictionary<string, string>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };

            const string ExpectedContentValue = "Key1=Value1&Key2=Value2";
            var actualBody = RestCallBody.CreateApplicationWwwFormUrlEncoded(dict);

            // Act
            var actualHttpContent = actualBody.CreateHttpContent();

            // Assert
            var actualContentValue = await actualHttpContent.ReadAsStringAsync();

            Assert.IsNotNull(actualContentValue);
            Assert.AreEqual(ExpectedContentValue, actualContentValue);
        }
    }
}