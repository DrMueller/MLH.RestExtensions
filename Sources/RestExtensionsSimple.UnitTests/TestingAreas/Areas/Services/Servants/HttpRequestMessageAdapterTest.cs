using System.Threading.Tasks;
using FluentAssertions;
using Mmu.Mlh.RestExtensionsSimple.Areas.Services.Servants.Implementation;
using Newtonsoft.Json;
using Xunit;

namespace Mmu.Mlh.RestExtensionsSimple.UnitTests.TestingAreas.Areas.Services.Servants
{
    public partial class HttpRequestMessageAdapterTest
    {
        private readonly HttpRequestMessageAdapter _sut;

        public HttpRequestMessageAdapterTest()
        {
            _sut = new HttpRequestMessageAdapter();
        }

        [Fact]
        public void Adapting_AdaptsRequestUri()
        {
            // Arrange
            var call = CreateGetCall();

            // Act
            var actualMessage = _sut.Adapt(call);

            // Assert
            actualMessage.RequestUri.ToString().Should().Be(call.RequestUri);
        }

        [Fact]
        public async Task Adapting_BodyBeingAnObject_SerizaliesContentToJson()
        {
            // Arrange
            var testDto = new BodyTestDto
            {
                Name = "Name1",
                SomeNumber = 123
            };

            var expectedBodyString = JsonConvert.SerializeObject(testDto);

            var call = CreatePostCall(testDto);

            // Act
            var actualMessage = _sut.Adapt(call);
            var actualBody = await actualMessage.Content.ReadAsStringAsync();

            // Assert
            actualBody.Should().Be(expectedBodyString);
        }

        [Fact]
        public void Adapting_BodyBeingNull_SetsContentAsNull()
        {
            // Arrange
            var call = CreatePostCall(null);

            // Act
            var actualMessage = _sut.Adapt(call);

            // Assert
            actualMessage.Content.Should().BeNull();
        }

        [Fact]
        public async Task Adapting_BodyBeingString_SetsContentAsIs()
        {
            // Arrange
            const string BodyString = "Hello World";
            var call = CreatePostCall(BodyString);

            // Act
            var actualMessage = _sut.Adapt(call);
            var actualBody = await actualMessage.Content.ReadAsStringAsync();

            // Assert
            actualBody.Should().Be(BodyString);
        }

        [Fact]
        public void Adapting_WithBody_SetsMeiaContentTypeAsApplicationJson()
        {
            // Arrange
            const string Body = "Hello World";
            var call = CreatePostCall(Body);

            // Act
            var actualMessage = _sut.Adapt(call);

            // Assert
            actualMessage.Content.Headers.ContentType.MediaType.Should().Be(HttpRequestMessageAdapter.ContentMediaType);
        }
    }
}