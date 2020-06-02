using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Newtonsoft.Json;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants.Implementation
{
    internal class RestCallResultAdapter : IRestCallResultAdapter
    {
        public RestCallResult AdaptResult(HttpResponseMessage response)
        {
            return new RestCallResult((int)response.StatusCode, response.ReasonPhrase);
        }

        public async Task<RestCallResult<T>> AdaptResultAsync<T>(HttpResponseMessage response)
        {
            var content = default(T);
            if (response.IsSuccessStatusCode)
            {
                content = await AdaptResultContentAsync<T>(response.Content);
            }

            return new RestCallResult<T>((int)response.StatusCode, response.ReasonPhrase, content);
        }

        private static async Task<T> AdaptResultContentAsync<T>(HttpContent content)
        {
            if (content == null)
            {
                return default;
            }

            var stringContent = await content.ReadAsStringAsync();

            var targetType = typeof(T);
            if (targetType.IsPrimitive || targetType == typeof(string))
            {
                return (T)Convert.ChangeType(stringContent, typeof(T));
            }

            if (string.IsNullOrEmpty(stringContent) || stringContent == "[]")
            {
                return default;
            }

            var result = JsonConvert.DeserializeObject<T>(stringContent);
            return result;
        }
    }
}