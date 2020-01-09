using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants.Implementation;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.RestProxies.Services.Servants
{
    [TestFixture]
    public class RestCallResultAdapterUnitTests
    {
        private RestCallResultAdapter _sut;

        [SetUp]
        public void Align()
        {
            _sut = new RestCallResultAdapter();
        }

        [Test]
        public async Task AdaptingGeneric_BodyBeingNull_SetsContentAsNull()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = null
            };

            // Act
            var actualResult = await _sut.AdaptResultAsync<ContentObject>(response);

            // Assert
            Assert.IsNull(actualResult.Content);
        }

        [Test]
        public async Task AdaptingGeneric_BodyBeinJsonString_SetsContentAsObject()
        {
            // Arrange
            var obj = new ContentObject
            {
                Height = 123,
                Name = "Name123"
            };

            var jsonObj = JsonConvert.SerializeObject(obj);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonObj)
            };

            // Act
            var actualResult = await _sut.AdaptResultAsync<ContentObject>(response);

            // Assert
            Assert.IsNotNull(actualResult.Content);
            Assert.AreEqual(obj.Height, actualResult.Content.Height);
            Assert.AreEqual(obj.Name, actualResult.Content.Name);
        }

        [Test]
        public void AdaptingNonGeneric_Adapts()
        {
            // Arrange
            const string ExpectedMessage = "Hello Test";
            const HttpStatusCode ExpectedCode = HttpStatusCode.Ambiguous;

            var response = new HttpResponseMessage(ExpectedCode)
            {
                ReasonPhrase = ExpectedMessage
            };

            // Act
            var actualResult = _sut.AdaptResult(response);

            // Assert
            Assert.AreEqual(ExpectedMessage, actualResult.ReturnMessage);
            Assert.AreEqual((int)ExpectedCode, actualResult.StatusCode);
            Assert.IsFalse(actualResult.WasSuccess);
        }
    }
}