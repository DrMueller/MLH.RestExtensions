using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.RestExtensions.Areas.Models
{
    public class RestHeaders
    {
        public IReadOnlyCollection<RestHeader> Entries { get; }

        public RestHeaders(IReadOnlyCollection<RestHeader> entries)
        {
            Entries = entries;
            Guard.ObjectNotNull(() => entries);
        }

        public static RestHeaders CreateNone()
        {
            return new RestHeaders(new List<RestHeader>());
        }
    }
}