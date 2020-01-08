using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants
{
    public interface IHttpClientProxy : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}