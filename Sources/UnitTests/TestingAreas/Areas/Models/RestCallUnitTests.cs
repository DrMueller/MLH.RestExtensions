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
        public void Constructor_WithQueryParameters_AddsQueryParametersToFullUri()
        {
            const string UriString = "https://www.google.ch";
            var baseUri = new Uri(UriString);

            const RestCallMethodType MethodType = RestCallMethodType.Get;
            var security = RestSecurity.CreateAnonymous();
            var headers = new RestHeaders(new List<RestHeader>());
            var body = Maybe.CreateSome(RestCallBody.CreateApplicationJson(new object()));

            var queryParameterList = new List<QueryParameter>
            {
                new QueryParameter("Key1", "Value1", "Value2"),
                new QueryParameter("Key2", "Value3")
            };

            const string ExpectedUriString = UriString + "?Key1=Value1&Key1=Value2&Key2=Value3";
            var expectedFullUri = new Uri(ExpectedUriString);

            var queryParameters = new QueryParameters(queryParameterList);

            ConstructorTestBuilderFactory.Constructing<RestCall>()
                .UsingDefaultConstructor()
                .WithArgumentValues(baseUri, Maybe.CreateNone<string>(), MethodType, security, headers, body, queryParameters)
                .Maps()
                .ToProperty(f => f.AbsoluteUri).WithValue(expectedFullUri)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public void Constructor_Works()
        {
            var baseUri = new Uri("https://www.google.ch");
            const string ActualResourcePathString = "Test";

            Maybe<string> resourcePath = ActualResourcePathString;
            const RestCallMethodType MethodType = RestCallMethodType.Get;
            var security = RestSecurity.CreateAnonymous();
            var headers = new RestHeaders(new List<RestHeader>());
            var body = Maybe.CreateSome(RestCallBody.CreateApplicationJson(new object()));
            var expectedFullUri = new Uri(baseUri, "/" + ActualResourcePathString);
            var queryParameters = new QueryParameters(new List<QueryParameter>());

            ConstructorTestBuilderFactory.Constructing<RestCall>()
                .UsingDefaultConstructor()
                .WithArgumentValues(baseUri, resourcePath, MethodType, security, headers, body, queryParameters)
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