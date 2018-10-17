using System;
using Mmu.Mlh.RestExtensions.Areas.Models;

namespace Mmu.Mlh.RestExtensions.Areas.RestCallBuilding
{
    public interface IRestCallBuilderFactory
    {
        IRestCallBuilder StartBuilding(Uri basePath, RestCallMethodType methodType = RestCallMethodType.Get);
    }
}