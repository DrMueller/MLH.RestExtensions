using System;
using JetBrains.Annotations;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models.RestCallBodies.TestingModels
{
    [UsedImplicitly]
    internal class Individual
    {
        [UsedImplicitly]
        public DateTime Birthdate { get; set; }

        [UsedImplicitly]
        public string FirstName { get; set; }

        [UsedImplicitly]
        public string LastName { get; set; }

        internal static Individual CreateOne()
        {
            return new Individual { Birthdate = new DateTime(1986, 12, 29), FirstName = "Matthias", LastName = "Müller" };
        }
    }
}