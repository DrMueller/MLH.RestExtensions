using System.Net.Http;
using Mmu.Mlh.RestExtensions.Areas.Models.Security.SecurityTypes;

namespace Mmu.Mlh.RestExtensions.Areas.Models.Security
{
    public abstract class RestSecurity
    {
        public static RestSecurity CreateAnonymous()
        {
            return new Anonymous();
        }

        public static RestSecurity CreateBasicAuthentication(string userName, string password)
        {
            return new BasicAuthentication(userName, password);
        }

        public static RestSecurity CreateTokenSecurity(string encodedToken)
        {
            return new TokenSecurity(encodedToken);
        }

        internal abstract void ApplySecurity(HttpRequestMessage requestMessage);
    }
}