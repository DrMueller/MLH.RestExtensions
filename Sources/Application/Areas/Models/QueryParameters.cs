using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.RestExtensions.Areas.Models
{
    public class QueryParameters
    {
        private readonly IReadOnlyCollection<QueryParameter> _entries;

        public QueryParameters(IReadOnlyCollection<QueryParameter> entries)
        {
            Guard.ObjectNotNull(() => entries);

            _entries = entries;
        }

        internal void AppendQueryParameters(StringBuilder urlBuilder)
        {
            if (!_entries.Any())
            {
                return;
            }

            var qryStringBuilder = new StringBuilder("?");
            _entries.ForEach(entry => entry.Append(qryStringBuilder));
            qryStringBuilder.Remove(qryStringBuilder.Length - 1, 1);
            var resultString = qryStringBuilder.ToString();
            urlBuilder.Append(resultString);
        }
    }
}