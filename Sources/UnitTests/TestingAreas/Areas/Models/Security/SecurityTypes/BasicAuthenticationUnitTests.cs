using System.Net.Http;
using Mmu.Mlh.RestExtensions.Areas.Models.Security.SecurityTypes;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models.Security.SecurityTypes
{
    [TestFixture]
    public class BasicAuthenticationUnitTests
    {
        [Test]
        public void ApplyingSecurity_SetsAuthorizationHeader_ToBasicAuthentication()
        {
            // Arrange
            var basicAuth = new BasicAuthentication("TestUser", "TestPassword");
            var request = new HttpRequestMessage();
            const string ExpectedKey = "VGVzdFVzZXI6VGVzdFBhc3N3b3Jk";

            // Act
            basicAuth.ApplySecurity(request);
            var actualAuthorizationHeader = request.Headers.Authorization;

            // Assert
            Assert.IsNotNull(actualAuthorizationHeader);
            Assert.AreEqual("Basic", actualAuthorizationHeader.Scheme);
            Assert.AreEqual(ExpectedKey, actualAuthorizationHeader.Parameter);
        }
    }
}