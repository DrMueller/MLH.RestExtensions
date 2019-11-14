using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.RestExtensions.Areas.Models.RestCallBodies
{
    public class ApplicationWwwFormUrlEncodedBody : RestCallBody
    {
        private readonly IDictionary<string, string> _keyValuePairs;
        public override string MediaType { get; } = "application/x-www-form-urlencoded";

        internal ApplicationWwwFormUrlEncodedBody(IDictionary<string, string> keyValuePairs) : base(keyValuePairs)
        {
            Guard.ObjectNotNull(() => keyValuePairs);
            _keyValuePairs = keyValuePairs;
        }

        protected override HttpContent CreateWHttpContentWithPayload()
        {
            var payloadString = SerializePayload();
            var httpContent = new StringContent(payloadString);
            return httpContent;
        }

        private string SerializePayload()
        {
            var stringBuilder = new StringBuilder();

            foreach (var current in _keyValuePairs)
            {
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append("&");
                }

                stringBuilder.AppendFormat("{0}{1}{2}", HttpUtility.UrlEncode(current.Key), "=", HttpUtility.UrlEncode(current.Value));
            }

            var result = stringBuilder.ToString();
            return result;
        }
    }
}