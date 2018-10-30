using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants
{
    public interface IHttpClientProxy : IDisposable
    {
        Task<HttpResponse> SendAsync(HttpRequestMessage request);
    }
}