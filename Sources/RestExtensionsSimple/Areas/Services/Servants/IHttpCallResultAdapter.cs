using System.Net.Http;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensionsSimple.Areas.Models;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Services.Servants
{
    public interface IHttpCallResultAdapter
    {
        HttpCallResult AdaptResult(HttpResponseMessage response);

        Task<HttpCallResult<T>> AdaptResultAsync<T>(HttpResponseMessage response);
    }
}