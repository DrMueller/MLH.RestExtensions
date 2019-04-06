using System.Net.Http;
using Newtonsoft.Json;

namespace Mmu.Mlh.RestExtensions.Areas.Models.RestCallBodies
{
    public class ApplicationJsonBody : RestCallBody
    {
        public const string MediaTypeAppJson = "application/json";

        public override string MediaType { get; } = MediaTypeAppJson;

        public ApplicationJsonBody(object payload) : base(payload)
        {
        }

        protected override HttpContent CreateWHttpContentWithPayload()
        {
            string jsonBody;
            if (Payload is string s)
            {
                jsonBody = s;
            }
            else
            {
                jsonBody = JsonConvert.SerializeObject(Payload);
            }

            var result = new StringContent(jsonBody);
            return result;
        }
    }
}