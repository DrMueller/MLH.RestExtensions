using System;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models.RestCallBodies.TestingModels
{
    internal class Individual
    {
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        internal static Individual CreateOne()
        {
            return new Individual
            {
                Birthdate = new DateTime(1986, 12, 29),
                FirstName = "Matthias",
                LastName = "Müller"
            };
        }
    }
}