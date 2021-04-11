using System.Net.Http;
using Mmu.Mlh.RestExtensionsSimple.Areas.Models;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Services.Servants
{
    public interface IHttpRequestMessageAdapter
    {
        HttpRequestMessage Adapt(HttpCall httpCall);
    }
}