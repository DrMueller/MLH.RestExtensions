using System;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding.Implementation
{
    internal class RestCallBuilderFactory : IRestCallBuilderFactory
    {
        public IRestCallBuilder StartBuilding(Uri basePath, RestCallMethodType methodType = RestCallMethodType.Get)
        {
            return new RestCallBuilder(basePath, methodType);
        }
    }
}