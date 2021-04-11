using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Models
{
    public class BasicAuthCredentials
    {
        public string Password { get; }
        public string UserName { get; }

        public BasicAuthCredentials(string userName, string password)
        {
            Guard.StringNotNullOrEmpty(() => userName);
            Guard.StringNotNullOrEmpty(() => password);

            UserName = userName;
            Password = password;
        }
    }
}