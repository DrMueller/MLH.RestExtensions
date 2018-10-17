using System.Net.Http;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestProxies.Servants
{
    internal interface IHttpRequestFactory
    {
        HttpRequestMessage Create(RestCall restCall);
    }
}