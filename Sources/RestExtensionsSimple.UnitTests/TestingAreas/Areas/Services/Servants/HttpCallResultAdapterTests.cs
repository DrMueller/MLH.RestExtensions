using System;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Mmu.Mlh.RestExtensionsSimple.Areas.Services.Servants.Implementation;
using Xunit;

namespace Mmu.Mlh.RestExtensionsSimple.UnitTests.TestingAreas.Areas.Services.Servants
{
    public class HttpCallResultAdapterTests
    {
        private readonly HttpCallResultAdapter _sut;

        public HttpCallResultAdapterTests()
        {
            _sut = new HttpCallResultAdapter();
        }

        [Fact]
        public void AdaptingResult_AdaptsResult()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = Guid.NewGuid().ToString()
            };

            // Act
            var actualCallResult = _sut.AdaptResult(responseMessage);

            // Assert
            actualCallResult.ReturnMessage.Should().Be(responseMessage.ReasonPhrase);
            actualCallResult.WasSuccess.Should().BeTrue();
            actualCallResult.StatusCode.Should().Be((int)responseMessage.StatusCode);
        }
    }
}