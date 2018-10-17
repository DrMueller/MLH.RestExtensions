using System.Net.Http;

namespace Mmu.Mlh.RestExtensions.Areas.Models.Security.SecurityTypes
{
    public class Anonymouss : RestSecurity
    {
        internal Anonymouss()
        {
        }

        internal override void ApplySecurity(HttpRequestMessage requestMessage)
        {
            // No security, nothing to do
        }
    }
}