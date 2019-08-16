using System;
using System.Collections.Generic;
using System.Linq;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding.Implementation
{
    internal class RestCallBuilder : IRestCallBuilder
    {
        private readonly Uri _basePath;
        private readonly RestCallMethodType _methodType;
        private readonly List<QueryParameterBuilder> _queryParamBuilders = new List<QueryParameterBuilder>();
        private readonly List<RestHeadersBuilder> _restHeaderBuilders = new List<RestHeadersBuilder>();
        private Maybe<RestCallBody> _body = Maybe.CreateNone<RestCallBody>();
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
                new RestHeaders(_restHeaderBuilders.SelectMany(f => f.Build()).ToList()),
                _body,
                new QueryParameters(_queryParamBuilders.SelectMany(f => f.Build()).ToList()));
        }

        public IRestCallBuilder WithBody(RestCallBody body)
        {
            _body = body;
            return this;
        }

        public IRestHeadersBuilder WithHeaders()
        {
            var restHeaderBuilder = new RestHeadersBuilder(this);
            _restHeaderBuilders.Add(restHeaderBuilder);
            return restHeaderBuilder;
        }

        public IQueryParameterBuilder WithQueryParameters()
        {
            var queryParamBuilder = new QueryParameterBuilder(this);
            _queryParamBuilders.Add(queryParamBuilder);
            return queryParamBuilder;
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