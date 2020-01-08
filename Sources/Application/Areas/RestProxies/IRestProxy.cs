using System;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies
{
    public interface IRestProxy : IDisposable
    {
        Task<RestCallResult<T>> PerformCallAsync<T>(RestCall restCall);

        Task<RestCallResult> PerformCallAsync(RestCall restCall);
    }
}