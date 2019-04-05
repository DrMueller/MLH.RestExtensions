using Mmu.Mlh.RestExtensions.Areas.Models.Security;
using Mmu.Mlh.RestExtensions.Areas.Models.Security.SecurityTypes;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models.Security
{
    [TestFixture]
    public class RestSecurityUnitTests
    {
        [Test]
        public void CreatingAnonymous_CreatesAnonymous()
        {
            // Act
            var actualSecurity = RestSecurity.CreateAnonymous();

            // Assert
            Assert.IsInstanceOf<Anonymous>(actualSecurity);
        }

        [Test]
        public void CreatingBasicAuthentication_CreatesBasicAuthentication()
        {
            // Act
            var actualSecurity = RestSecurity.CreateBasicAuthentication("tra", "tra");

            // Assert
            Assert.IsInstanceOf<BasicAuthentication>(actualSecurity);
        }

        [Test]
        public void CreatingTokenSecurity_CreatesTokenSecurity()
        {
            // Act
            var actualSecurity = RestSecurity.CreateTokenSecurity("tra");

            // Assert
            Assert.IsInstanceOf<TokenSecurity>(actualSecurity);
        }
    }
}