using System.Net.Http;

namespace Mmu.Mlh.RestExtensions.Areas.Models.Security.SecurityTypes
{
    public class Anonymous : RestSecurity
    {
        internal Anonymous()
        {
        }

        internal override void ApplySecurity(HttpRequestMessage requestMessage)
        {
            // No security, nothing to do
        }
    }
}