using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.RestExtensions.Areas.Models
{
    public class QueryParameter
    {
        private readonly string _key;
        private readonly IReadOnlyCollection<string> _values;

        public QueryParameter(string key, params object[] values)
        {
            Guard.StringNotNullOrEmpty(() => key);
            Guard.ObjectNotNull(() => values);

            _key = Uri.EscapeDataString(key);
            _values = values
                .Select(val => val.ToString())
                .Select(Uri.EscapeUriString)
                .ToList();
        }

        internal void Append(StringBuilder sb)
        {
            foreach (var value in _values)
            {
                sb.Append(_key);
                sb.Append("=");
                sb.Append(value);
                sb.Append("&");
            }
        }
    }
}