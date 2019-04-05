using System.Net.Http;
using Mmu.Mlh.RestExtensions.Areas.Models.Security.SecurityTypes;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models.Security.SecurityTypes
{
    [TestFixture]
    public class TokenSecurityUnitTests
    {
        [Test]
        public void ApplyingSecurity_SetsAuthorizationHeader_ToTokenSecurity()
        {
            // Arrange
            const string Token = "Test1234";
            var tokenSec = new TokenSecurity(Token);
            var request = new HttpRequestMessage();

            // Act
            tokenSec.ApplySecurity(request);
            var actualAuthorizationHeader = request.Headers.Authorization;

            // Assert
            Assert.IsNotNull(actualAuthorizationHeader);
            Assert.IsNull(actualAuthorizationHeader.Parameter);
            Assert.AreEqual(Token, actualAuthorizationHeader.Scheme);
        }
    }
}