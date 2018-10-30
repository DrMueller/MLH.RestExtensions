using System;
using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.RestProxies.Services
{
    internal static class DataGenerator
    {
        internal static RestCall CreateDefaultRestCall()
        {
            return new RestCall(
                new Uri("https://www.google.ch"),
                Maybe.CreateNone<string>(),
                RestCallMethodType.Get,
                RestSecurity.CreateAnonymous(),
                new RestHeaders(new List<RestHeader>()),
                Maybe.CreateNone<RestCallBody>(),
                new QueryParameters(new List<QueryParameter>()));
        }
    }
}