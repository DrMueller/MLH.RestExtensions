using System.Collections.Generic;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding.Implementation
{
    internal class QueryParameterBuilder : IQueryParameterBuilder
    {
        private readonly List<QueryParameter> _queryParameters = new List<QueryParameter>();
        private readonly IRestCallBuilder _restCallBuilder;

        public QueryParameterBuilder(IRestCallBuilder restCallBuilder)
        {
            _restCallBuilder = restCallBuilder;
        }

        public IRestCallBuilder BuildQueryParameters()
        {
            return _restCallBuilder;
        }

        public QueryParameters FinishBuilding()
        {
            return new QueryParameters(_queryParameters);
        }

        public IQueryParameterBuilder WithQueryParameter(string key, params object[] values)
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