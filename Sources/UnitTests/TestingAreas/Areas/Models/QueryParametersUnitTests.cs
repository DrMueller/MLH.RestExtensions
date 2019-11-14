using System.Collections.Generic;
using System.Text;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.UnitTests.TestingAreas.Areas.Models
{
    [TestFixture]
    public class QueryParametersUnitTests
    {
        [Test]
        public void AppendingQueryParameters_HavingNone_DoesNotAppendQueryParameters()
        {
            // Arrange
            var queryParameters = new QueryParameters(new List<QueryParameter>());
            var sb = new StringBuilder();

            // Act
            queryParameters.AppendQueryParameters(sb);

            // Assert
            var actualString = sb.ToString();
            Assert.IsEmpty(actualString);
        }

        [Test]
        public void AppendingQueryParameters_HavingQueryParameters_WithoutValues_DoesNotAppendQueryParameters()
        {
            // Arrange
            var queryParamsList = new List<QueryParameter> { new QueryParameter("Key1") };

            var queryParameters = new QueryParameters(queryParamsList);
            var sb = new StringBuilder();

            // Act
            queryParameters.AppendQueryParameters(sb);

            // Assert
            var actualString = sb.ToString();
            Assert.IsEmpty(actualString);
        }

        [Test]
        public void AppendingQueryParameters_HavingQueryParameters_WithValues_AppendsQueryParameters()
        {
            // Arrange
            var queryParamsList = new List<QueryParameter> { new QueryParameter("Key1", "Val1", "Val2"), new QueryParameter("Key2", "Val3") };

            const string ExpectedQueryParamStr = "?Key1=Val1&Key1=Val2&Key2=Val3";

            var queryParameters = new QueryParameters(queryParamsList);
            var sb = new StringBuilder();

            // Act
            queryParameters.AppendQueryParameters(sb);

            // Assert
            var actualString = sb.ToString();
            Assert.IsNotEmpty(actualString);
            Assert.AreEqual(ExpectedQueryParamStr, actualString);
        }

        [Test]
        public void Constructor_Works()
        {
            ConstructorTestBuilderFactory.Constructing<QueryParameters>()
                .UsingDefaultConstructor()
                .WithArgumentValues(new List<QueryParameter>())
                .Succeeds()
                .WithArgumentValues(null)
                .Fails()
                .Assert();
        }
    }
}