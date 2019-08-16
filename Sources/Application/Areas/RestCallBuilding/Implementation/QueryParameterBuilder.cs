using System;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding.Implementation
{
    internal class QueryParameterBuilder : IQueryParameterBuilder
    {
        public QueryParameterBuilder(IRestCallBuilder restCallBuilder)
        {
            _restCallBuilder = restCallBuilder;
        }

        public IRestCallBuilder Build()
        {
            return _restCallBuilder;
        }

        public IQueryParameterBuilder WithQueryParameter(string key, params object[] values)
        {
            throw new NotImplementedException();
        }

        private readonly IRestCallBuilder _restCallBuilder;
    }
}