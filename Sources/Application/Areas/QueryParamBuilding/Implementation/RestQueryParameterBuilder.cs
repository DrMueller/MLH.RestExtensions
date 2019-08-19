using System.Collections.Generic;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding;

namespace Mmu.Mlh.RestExtensions.Areas.QueryParamBuilding.Implementation
{
    internal class RestQueryParameterBuilder : IRestQueryParameterBuilder
    {
        private readonly List<QueryParameter> _queryParameters = new List<QueryParameter>();
        private readonly IRestCallBuilder _restCallBuilder;

        public RestQueryParameterBuilder(IRestCallBuilder restCallBuilder)
        {
            _restCallBuilder = restCallBuilder;
        }

        public IRestCallBuilder BuildQueryParameters()
        {
            return _restCallBuilder;
        }

        public IRestQueryParameterBuilder WithQueryParameter(string key, params object[] values)
        {
            _queryParameters.Add(new QueryParameter(key, values));
            return this;
        }

        internal IReadOnlyCollection<QueryParameter> Build()
        {
            return _queryParameters;
        }
    }
}