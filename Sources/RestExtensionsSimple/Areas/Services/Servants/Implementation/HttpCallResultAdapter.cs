using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensionsSimple.Areas.Models;
using Newtonsoft.Json;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Services.Servants.Implementation
{
    public class HttpCallResultAdapter : IHttpCallResultAdapter
    {
        public HttpCallResult AdaptResult(HttpResponseMessage response)
        {
            return new HttpCallResult((int)response.StatusCode, response.ReasonPhrase);
        }

        public async Task<HttpCallResult<T>> AdaptResultAsync<T>(HttpResponseMessage response)
        {
            var content = default(T);
            if (response.IsSuccessStatusCode)
            {
                content = await ReadResultContentAsync<T>(response);
            }

            return new HttpCallResult<T>((int)response.StatusCode, response.ReasonPhrase, content);
        }

        private static async Task<T> ReadResultContentAsync<T>(HttpResponseMessage response)
        {
            var stringContent = await response.Content.ReadAsStringAsync();
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