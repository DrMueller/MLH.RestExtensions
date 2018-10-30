using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Implementation;
using Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.RestProxies.Services
{
    [TestFixture]
    public class RestProxyUnitTests
    {
        private Mock<IHttpClientProxyFactory> _httpClientProxyFactoryMock;
        private Mock<IHttpRequestFactory> _httpRequestFactoryMock;
        private Mock<IRestCallBuilderFactory> _restCallBuilderFactoryMock;
        private RestProxy _sut;

        [SetUp]
        public void Align()
        {
            _httpRequestFactoryMock = new Mock<IHttpRequestFactory>();
            _restCallBuilderFactoryMock = new Mock<IRestCallBuilderFactory>();
            _httpClientProxyFactoryMock = new Mock<IHttpClientProxyFactory>();

            _sut = new RestProxy(
                _httpRequestFactoryMock.Object,
                _restCallBuilderFactoryMock.Object,
                _httpClientProxyFactoryMock.Object);
        }

        [Test]
        public async Task PerformingCall_CallsHttpClientProxyFactory_Once()
        {
            // Arrange
            var restCall = DataGenerator.CreateDefaultRestCall();
            var httpClientProxyMock = new Mock<IHttpClientProxy>();
            _httpClientProxyFactoryMock.Setup(f => f.Create()).Returns(httpClientProxyMock.Object);
            httpClientProxyMock.Setup(f => f.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(
                Task.FromResult(new HttpResponse(true, string.Empty)));

            // Act
            await _sut.PerformCallAsync<string>(restCall);

            // Assert
            _httpClientProxyFactoryMock.Verify(f => f.Create(), Times.Once);
        }

        [Test]
        public async Task PerformingCall_CallsHttpRequestFactoryOnce_WithPassedRestCall()
        {
            // Arrange
            var restCall = DataGenerator.CreateDefaultRestCall();
            var httpClientProxyMock = new Mock<IHttpClientProxy>();
            _httpClientProxyFactoryMock.Setup(f => f.Create()).Returns(httpClientProxyMock.Object);
            httpClientProxyMock.Setup(f => f.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(
                Task.FromResult(new HttpResponse(true, string.Empty)));

            // Act
            await _sut.PerformCallAsync<string>(restCall);

            // Assert
            _httpRequestFactoryMock.Verify(f => f.Create(restCall), Times.Once);
        }

        [Test]
        public void PerformingCall_ReturningStatusCodeNotSuccessful_ThrowsRestCallException()
        {
            // Arrange
            var restCall = DataGenerator.CreateDefaultRestCall();
            var httpClientProxyMock = new Mock<IHttpClientProxy>();
            _httpClientProxyFactoryMock.Setup(f => f.Create()).Returns(httpClientProxyMock.Object);
            httpClientProxyMock.Setup(f => f.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(
                Task.FromResult(new HttpResponse(false, string.Empty)));

            // Act & Assert
            Assert.ThrowsAsync<RestCallException>(() => _sut.PerformCallAsync<string>(restCall));
        }

        [Test]
        public void PerformingCall_WithRestCallNull_ThrowsArgumentException()
        {
            // Arrange
            RestCall nullRestCall = null;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _sut.PerformCallAsync<string>(nullRestCall));
        }
    }
}