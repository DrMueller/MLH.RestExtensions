using System;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;

namespace Mmu.Mlh.RestExtensions.Areas.Models
{
    public class RestCall
    {
        public Uri AbsoluteUri
        {
            get
            {
                var actualResourcePath = ResourcePath.Reduce(string.Empty);
                var fullUrl = BaseUri.ToString();
                var baseUriEndsWithSlash = BaseUri.AbsolutePath.EndsWith("/", StringComparison.OrdinalIgnoreCase);
                var resourcePathStartsWithSlash = actualResourcePath.StartsWith("/", StringComparison.OrdinalIgnoreCase);

                if (!baseUriEndsWithSlash && !resourcePathStartsWithSlash)
                {
                    fullUrl += "/";
                }

                fullUrl += actualResourcePath;
                return new Uri(fullUrl);
            }
        }

        public Uri BaseUri { get; }
        public Maybe<RestCallBody> Body { get; }
        public RestHeaders Headers { get; }
        public RestCallMethodType MethodType { get; }
        public Maybe<string> ResourcePath { get; }
        public RestSecurity Security { get; }

        internal RestCall(Uri baseUri, Maybe<string> resourcePath, RestCallMethodType methodType, RestSecurity security, RestHeaders headers, Maybe<RestCallBody> body)
        {
            Guard.ObjectNotNull(() => baseUri);
            Guard.ObjectNotNull(() => security);
            Guard.ObjectNotNull(() => headers);
            Guard.ObjectNotNull(() => body);

            ResourcePath = resourcePath;
            MethodType = methodType;
            Security = security;
            Headers = headers;
            Body = body;
            BaseUri = baseUri;
        }
    }
}