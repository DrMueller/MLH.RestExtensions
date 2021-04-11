using System;
using Mmu.Mlh.RestExtensionsSimple.Areas.Models;

namespace Mmu.Mlh.RestExtensionsSimple.UnitTests.TestingAreas.Areas.Services.Servants
{
    public partial class HttpRequestMessageAdapterTest
    {
        private static HttpCall CreateCall(HttpCallMethodType type, object body)
        {
            return new HttpCall(
                "https://www.google.ch/",
                type,
                new BasicAuthCredentials(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                body);
        }

        private static HttpCall CreatePostCall(object body)
        {
            return CreateCall(HttpCallMethodType.Post, body);
        }

        private static HttpCall CreateGetCall()
        {
            return CreateCall(HttpCallMethodType.Get, null);
        }
    }
}