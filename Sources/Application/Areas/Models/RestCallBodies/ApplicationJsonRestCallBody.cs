using System.Net.Http;
using Newtonsoft.Json;

namespace Mmu.Mlh.RestExtensions.Areas.Models.RestCallBodies
{
    public class ApplicationJsonRestCallBody : RestCallBody
    {
        public override string MediaType { get; } = "application/json";

        public ApplicationJsonRestCallBody(object payload) : base(payload)
        {
        }

        protected override HttpContent CreateWHttpContentWithPayload()
        {
            var jsonBody = JsonConvert.SerializeObject(Payload);
            var result = new StringContent(jsonBody);
            return result;
        }
    }
}