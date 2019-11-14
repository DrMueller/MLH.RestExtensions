using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.Exceptions;
using Mmu.Mlh.RestExtensions.Areas.RestProxies;
using Mmu.Mlh.RestExtensions.IntegrationTests.TestingInfrastructure.Models;
using Mmu.Mlh.TestingExtensions.Areas.Common.BasesClasses;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.IntegrationTests.TestingAreas.Areas.RestProxies.Services
{
    [TestFixture]
    public class RestProxyIntegrationTests : TestingBaseWithContainer
    {
        private IRestProxy _sut;

        [SetUp]
        public void Align()
        {
            _sut = ServiceLocator.GetService<IRestProxy>();
        }

        [Test]
        public async Task PerformingCall_WithCorrectUrl_FetchesData()
        {
            // Arrange
            var restCall = DataGenerator.CreateGetOneTodoRestCall();

            // Act
            var actualResponse = await _sut.PerformCallAsync<Todo>(restCall);

            // Assert
            Assert.AreEqual(1, actualResponse.Id);
            Assert.AreEqual(1, actualResponse.UserId);
            Assert.AreEqual("delectus aut autem", actualResponse.Title);
            Assert.AreEqual(false, actualResponse.Completed);
        }

        [Test]
        public void PerformingCall_WithNotExistingUrl_ThrowsRestCallException()
        {
            // Arrange
            var restCall = DataGenerator.CreateNotExistingUrlRestCall();

            // Act
            Assert.ThrowsAsync<RestCallException>(() => _sut.PerformCallAsync<string>(restCall));
        }

        [Test]
        public async Task PerformingCall_WithQueryParameters_FetchesData_OfPostIdOne()
        {
            // Arrange
            var restCall = DataGenerator.CreateCommentsByPostIdRestCall();

            // Act
            var actualResponse = await _sut.PerformCallAsync<List<Post>>(restCall);

            // Assert
            Assert.AreEqual(5, actualResponse.Count);
            Assert.IsTrue(actualResponse.All(f => f.PostId == 1));
        }

        [Test]
        public async Task PerformingPost_WithApplicationJsonFromJsonString_SendsAsJson()
        {
            // Arrange
            var todo = new Todo
            {
                Completed = true,
                Title = "Hello Test",
                UserId = 123
            };

            var jsonString = JsonConvert.SerializeObject(todo);

            var restCall = DataGenerator.CreatePostOneTodoRestCall(jsonString);

            // Act
            var actualResponse = await _sut.PerformCallAsync<Todo>(restCall);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.IsTrue(actualResponse.Id > 0);
            Assert.AreEqual(todo.Completed, actualResponse.Completed);
            Assert.AreEqual(todo.Title, actualResponse.Title);
            Assert.AreEqual(todo.UserId, actualResponse.UserId);
        }

        [Test]
        public async Task PerformingPost_WithApplicationJsonFromObject_ConvertsAndSendsAsJson()
        {
            // Arrange
            var todo = new Todo
            {
                Completed = true,
                Title = "Hello Test",
                UserId = 123
            };

            var restCall = DataGenerator.CreatePostOneTodoRestCall(todo);

            // Act
            var actualResponse = await _sut.PerformCallAsync<Todo>(restCall);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.IsTrue(actualResponse.Id > 0);
            Assert.AreEqual(todo.Completed, actualResponse.Completed);
            Assert.AreEqual(todo.Title, actualResponse.Title);
            Assert.AreEqual(todo.UserId, actualResponse.UserId);
        }
    }
}