using System.Collections.Generic;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding.Implementation
{
    internal class RestHeadersBuilder : IRestHeadersBuilder
    {
        private readonly List<RestHeader> _headers = new List<RestHeader>();
        private readonly RestCallBuilder _restCallBuilder;

        public RestHeadersBuilder(RestCallBuilder restCallBuilder)
        {
            _restCallBuilder = restCallBuilder;
        }

        public IRestHeadersBuilder WithHeader(string name, string value)
        {
            _headers.Add(new RestHeader(name, value));
            return this;
        }

        public IRestCallBuilder BuildHeaders()
        {
            return _restCallBuilder;
        }

        internal IReadOnlyCollection<RestHeader> Build()
        {
            return _headers;
        }
    }
}