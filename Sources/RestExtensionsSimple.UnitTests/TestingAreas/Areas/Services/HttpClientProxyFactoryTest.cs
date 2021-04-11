using System;
using System.Net.Http;
using FluentAssertions;
using Mmu.Mlh.RestExtensionsSimple.Areas.Services.Implementation;
using Moq;
using Xunit;

namespace Mmu.Mlh.RestExtensionsSimple.UnitTests.TestingAreas.Areas.Services
{
    public class HttpClientProxyFactoryTest
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly HttpClientProxyFactory _sut;

        public HttpClientProxyFactoryTest()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            _sut = new HttpClientProxyFactory(
                _serviceProviderMock.Object,
                httpClientFactoryMock.Object);
        }

        [Fact]
        public void CreatingHttpClientProxy_ReturnsProxy()
        {
            // Arrange
            var proxy = new HttpClientProxy(null, null);
            _serviceProviderMock
                .Setup(f => f.GetService(typeof(HttpClientProxy)))
                .Returns(proxy);

            // Act
            var actualProxy = _sut.Create();

            // Assert
            actualProxy.Should().Be(proxy);
        }
    }
}