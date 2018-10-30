using System;
using System.Text;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;

namespace Mmu.Mlh.RestExtensions.Areas.Models
{
    public class RestCall
    {
        private readonly QueryParameters _queryParameters;

        public Uri AbsoluteUri
        {
            get
            {
                var sb = new StringBuilder(BaseUri.ToString());
                var actualResourcePath = ResourcePath.Reduce(string.Empty);
                var baseUriEndsWithSlash = BaseUri.AbsolutePath.EndsWith("/", StringComparison.OrdinalIgnoreCase);
                var resourcePathStartsWithSlash = actualResourcePath.StartsWith("/", StringComparison.OrdinalIgnoreCase);
                var addTrailingSlash = !baseUriEndsWithSlash && !resourcePathStartsWithSlash && !string.IsNullOrEmpty(actualResourcePath);

                if (addTrailingSlash)
                {
                    sb.Append("/");
                }

                sb.Append(actualResourcePath);
                _queryParameters.AppendQueryParameters(sb);

                return new Uri(sb.ToString());
            }
        }

        public Uri BaseUri { get; }
        public Maybe<RestCallBody> Body { get; }
        public RestHeaders Headers { get; }
        public RestCallMethodType MethodType { get; }
        public Maybe<string> ResourcePath { get; }
        public RestSecurity Security { get; }

        public RestCall(Uri baseUri, Maybe<string> resourcePath, RestCallMethodType methodType, RestSecurity security, RestHeaders headers, Maybe<RestCallBody> body, QueryParameters queryParameters)
        {
            Guard.ObjectNotNull(() => baseUri);
            Guard.ObjectNotNull(() => security);
            Guard.ObjectNotNull(() => headers);
            Guard.ObjectNotNull(() => body);
            Guard.ObjectNotNull(() => queryParameters);

            _queryParameters = queryParameters;
            ResourcePath = resourcePath;
            MethodType = methodType;
            Security = security;
            Headers = headers;
            Body = body;
            BaseUri = baseUri;
        }
    }
}