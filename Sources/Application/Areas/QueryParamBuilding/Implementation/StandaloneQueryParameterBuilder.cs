using System.Collections.Generic;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding;

namespace Mmu.Mlh.RestExtensions.Areas.QueryParamBuilding.Implementation
{
    internal class StandaloneQueryParameterBuilder : IStandaloneQueryParameterBuilder
    {
        private readonly List<QueryParameter> _queryParameters = new List<QueryParameter>();

        public QueryParameters Build()
        {
            return new QueryParameters(_queryParameters);
        }

        public IStandaloneQueryParameterBuilder WithQueryParameter(string key, params object[] values)
        {
            _queryParameters.Add(new QueryParameter(key, values));
            return this;
        }
    }
}