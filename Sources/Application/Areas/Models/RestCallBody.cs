using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.RestExtensions.Areas.Models.RestCallBodies;

namespace Mmu.Mlh.RestExtensions.Areas.Models
{
    public abstract class RestCallBody
    {
        public abstract string MediaType { get; }
        public object Payload { get; }

        protected RestCallBody(object payload)
        {
            Guard.ObjectNotNull(() => payload);
            Payload = payload;
        }

        public static RestCallBody CreateApplicationJson(object payload)
        {
            return new ApplicationJsonBody(payload);
        }

        public static RestCallBody CreateApplicationWwwFormUrlEncoded(IDictionary<string, string> keyValuePairs)
        {
            return new ApplicationWwwFormUrlEncodedBody(keyValuePairs);
        }

        internal HttpContent CreateHttpContent()
        {
            var httpContent = CreateWHttpContentWithPayload();
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
            return httpContent;
        }

        protected abstract HttpContent CreateWHttpContentWithPayload();
    }
}