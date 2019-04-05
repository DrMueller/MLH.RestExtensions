using System.Net.Http;
using Mmu.Mlh.RestExtensions.Areas.Models.Security.SecurityTypes;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models.Security.SecurityTypes
{
    [TestFixture]
    public class AnonymousUnitTests
    {
        [Test]
        public void ApplyingSecurity_DoesNotSetAuthorizationHeader()
        {
            // Arrange
            var basicAuth = new Anonymous();
            var request = new HttpRequestMessage();

            // Act
            basicAuth.ApplySecurity(request);
            var actualAuthorizationHeader = request.Headers.Authorization;

            // Assert
            Assert.IsNull(actualAuthorizationHeader);
        }
    }
}