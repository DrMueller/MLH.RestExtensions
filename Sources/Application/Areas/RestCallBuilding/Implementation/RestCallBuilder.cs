using System;
using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding.Implementation
{
    internal class RestCallBuilder : IRestCallBuilder
    {
        private readonly Uri _basePath;
        private readonly List<RestHeader> _headers = new List<RestHeader>();
        private readonly RestCallMethodType _methodType;
        private Maybe<RestCallBody> _body = Maybe.CreateNone<RestCallBody>();
        private List<QueryParameter> _queryParameters = new List<QueryParameter>();
        private Maybe<string> _resourcePath = Maybe.CreateNone<string>();
        private RestSecurity _restSecurity = RestSecurity.CreateAnonymous();

        public RestCallBuilder(Uri basePath, RestCallMethodType methodType)
        {
            _basePath = basePath;
            _methodType = methodType;
        }

        public RestCall Build()
        {
            return new RestCall(
                _basePath,
                _resourcePath,
                _methodType,
                _restSecurity,
                new RestHeaders(_headers),
                _body,
                new QueryParameters(_queryParameters));
        }

        public IRestCallBuilder WithBody(RestCallBody body)
        {
            _body = body;
            return this;
        }

        public IRestHeadersBuilder WithHeaders()
        {
            return new RestHeadersBuilder(this, _headers);
        }

        public IRestCallBuilder WithQueryParameter(string key, params string[] values)
        {
            _queryParameters.Add(new QueryParameter(key, values));

            return this;
        }

        public IRestCallBuilder WithResourcePath(string resourcePath)
        {
            _resourcePath = resourcePath;
            return this;
        }

        public IRestCallBuilder WithSecurity(RestSecurity security)
        {
            _restSecurity = security;
            return this;
        }
    }
}