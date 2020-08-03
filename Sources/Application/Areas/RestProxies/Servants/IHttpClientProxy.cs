using System.Net.Http;
using System.Threading.Tasks;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants
{
    public interface IHttpClientProxy
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}