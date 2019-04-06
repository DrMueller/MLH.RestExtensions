using System.Collections.Generic;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models
{
    [TestFixture]
    public class RestHeadersUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            var entries = new List<RestHeader>
            {
                new RestHeader("Name1", "Value1"),
                new RestHeader("Name2", "Value2"),
                new RestHeader("Name3", "Value3")
            };

            ConstructorTestBuilderFactory.Constructing<RestHeaders>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null)
                .Fails()
                .WithArgumentValues(entries)
                .Maps()
                .ToProperty(f => f.Entries).WithValues(entries)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public void CreatingEmpty_CreatesHeadersWithoutEntries()
        {
            // Act
            var actualHeaders = RestHeaders.CreateNone();

            // Assert
            Assert.IsNotNull(actualHeaders.Entries);
            Assert.AreEqual(0, actualHeaders.Entries.Count);
        }
    }
}