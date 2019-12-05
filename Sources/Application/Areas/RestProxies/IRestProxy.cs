using System;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies
{
    public interface IRestProxy : IDisposable
    {
        Task<T> PerformCallAsync<T>(RestCall restCall);

        Task<T> PerformCallAsync<T>(Func<IRestCallBuilderFactory, RestCall> restCallBuilderCallback);
    }
}