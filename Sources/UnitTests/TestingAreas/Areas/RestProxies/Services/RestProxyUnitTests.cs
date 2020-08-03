using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Implementation;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.RestProxies.Services
{
    [TestFixture]
    public class RestProxyUnitTests
    {
        private Mock<IHttpClientProxy> _httpClientProxyMock;
        private Mock<IHttpRequestFactory> _httpRequestFactoryMock;
        private Mock<IRestCallResultAdapter> _restCallResultAdapterMock;
        private RestProxy _sut;

        [SetUp]
        public void Align()
        {
            _httpRequestFactoryMock = new Mock<IHttpRequestFactory>();
            _restCallResultAdapterMock = new Mock<IRestCallResultAdapter>();
            _httpClientProxyMock = new Mock<IHttpClientProxy>();

            _sut = new RestProxy(
                _httpRequestFactoryMock.Object,
                _restCallResultAdapterMock.Object,
                _httpClientProxyMock.Object);
        }

        [Test]
        public async Task PerformingCall_CallsHttpClientProxy_ForSending_Once()
        {
            // Arrange
            var restCall = DataGenerator.CreateDefaultRestCall();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Accepted);
            _httpClientProxyMock.Setup(f => f.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(
                Task.FromResult(responseMessage));

            // Act
            await _sut.SendAsync<string>(restCall);

            // Assert
            _httpClientProxyMock.Verify(f => f.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);
        }

        [Test]
        public async Task PerformingCall_CallsHttpRequestFactoryOnce_WithPassedRestCall()
        {
            // Arrange
            var restCall = DataGenerator.CreateDefaultRestCall();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Accepted);
            _httpClientProxyMock.Setup(f => f.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(
                Task.FromResult(responseMessage));

            // Act
            await _sut.SendAsync<string>(restCall);

            // Assert
            _httpRequestFactoryMock.Verify(f => f.Create(restCall), Times.Once);
        }

        [Test]
        public void PerformingCall_WithRestCallNull_ThrowsArgumentException()
        {
            // Arrange
            RestCall nullRestCall = null;

            // Act & Assert
            //// ReSharper disable once ExpressionIsAlwaysNull
            Assert.ThrowsAsync<ArgumentException>(() => _sut.SendAsync<string>(nullRestCall));
        }
    }
}