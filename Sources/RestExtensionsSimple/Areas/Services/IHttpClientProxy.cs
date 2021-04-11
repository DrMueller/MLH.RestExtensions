using System.Threading.Tasks;
using Mmu.Mlh.RestExtensionsSimple.Areas.Models;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Services
{
    public interface IHttpClientProxy
    {
        Task<HttpCallResult> SendAsync(HttpCall restCall);

        Task<HttpCallResult<T>> SendAsync<T>(HttpCall restCall);
    }
}