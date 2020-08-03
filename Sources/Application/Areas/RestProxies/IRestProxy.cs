using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies
{
    public interface IRestProxy
    {
        Task<RestCallResult<T>> SendAsync<T>(RestCall restCall);

        Task<RestCallResult> SendAsync(RestCall restCall);
    }
}