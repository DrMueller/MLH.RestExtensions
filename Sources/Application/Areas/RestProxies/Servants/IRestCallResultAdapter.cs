using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants
{
    public interface IRestCallResultAdapter
    {
        RestCallResult AdaptResult(HttpResponseMessage response);

        Task<RestCallResult<T>> AdaptResultAsync<T>(HttpResponseMessage response);
    }
}