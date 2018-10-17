using System;
using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models
{
    [TestFixture]
    public class RestCallUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            var baseUri = new Uri("https://www.google.ch");
            const string ActualResourcePathString = "Test";

            Maybe<string> resourcePath = ActualResourcePathString;
            const RestCallMethodType MethodType = RestCallMethodType.Get;
            var security = RestSecurity.CreateAnonymous();
            var headers = new RestHeaders(new List<RestHeader>());
            var body = Maybe.CreateSome(new RestCallBody(new object()));
            var expectedFullUri = new Uri(baseUri, "/" + ActualResourcePathString);

            ConstructorTestBuilderFactory.Constructing<RestCall>()
                .UsingDefaultConstructor()
                .WithArgumentValues(baseUri, resourcePath, MethodType, security, headers, body)
                .Maps()
                .ToProperty(f => f.BaseUri).WithValue(baseUri)
                .ToProperty(f => f.Body).WithValue(body)
                .ToProperty(f => f.Headers).WithValue(headers)
                .ToProperty(f => f.MethodType).WithValue(MethodType)
                .ToProperty(f => f.ResourcePath).WithValue(resourcePath)
                .ToProperty(f => f.Security).WithValue(security)
                .ToProperty(f => f.AbsoluteUri).WithValue(expectedFullUri)
                .BuildMaps()
                .Assert();
        }
    }
}