using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [SetUp]
        public void Align()
        {
            _sut = ServiceLocator.GetService<IRestProxy>();
        }

        private IRestProxy _sut;

        [Test]
        public async Task PerformingCall_WithCorrectUrl_FetchesData()
        {
            // Arrange
            var restCall = DataGenerator.CreateGetOneTodoRestCall();

            // Act
            var actualResponse = await _sut.PerformCallAsync<Todo>(restCall);
            var content = actualResponse.Content;

            // Assert
            Assert.IsNotNull(content);
            Assert.AreEqual(1, content.Id);
            Assert.AreEqual(1, content.UserId);
            Assert.AreEqual("delectus aut autem", content.Title);
            Assert.AreEqual(false, content.Completed);
        }

        [Test]
        public async Task PerformingCall_WithNotExistingUrl_ReturnsFailure()
        {
            // Arrange
            var restCall = DataGenerator.CreateNotExistingUrlRestCall();

            // Act
            var actualResponse = await _sut.PerformCallAsync(restCall);

            // Assert
            Assert.IsFalse(actualResponse.WasSuccess);
        }

        [Test]
        public async Task PerformingCall_WithQueryParameters_FetchesData_OfPostIdOne()
        {
            // Arrange
            var restCall = DataGenerator.CreateCommentsByPostIdRestCall();

            // Act
            var actualResponse = await _sut.PerformCallAsync<List<Post>>(restCall);
            var content = actualResponse.Content;

            // Assert
            Assert.AreEqual(5, content.Count);
            Assert.IsTrue(content.All(f => f.PostId == 1));
        }

        [Test]
        public async Task PerformingPost_WithApplicationJsonFromJsonString_SendsAsJson()
        {
            // Arrange
            var todo = new Todo { Completed = true, Title = "Hello Test", UserId = 123 };

            var jsonString = JsonConvert.SerializeObject(todo);

            var restCall = DataGenerator.CreatePostOneTodoRestCall(jsonString);

            // Act
            var actualResponse = await _sut.PerformCallAsync<Todo>(restCall);
            var content = actualResponse.Content;

            // Assert
            Assert.IsNotNull(content);
            Assert.IsTrue(content.Id > 0);
            Assert.AreEqual(todo.Completed, content.Completed);
            Assert.AreEqual(todo.Title, content.Title);
            Assert.AreEqual(todo.UserId, content.UserId);
        }

        [Test]
        public async Task PerformingPost_WithApplicationJsonFromObject_ConvertsAndSendsAsJson()
        {
            // Arrange
            var todo = new Todo { Completed = true, Title = "Hello Test", UserId = 123 };

            var restCall = DataGenerator.CreatePostOneTodoRestCall(todo);

            // Act
            var actualResponse = await _sut.PerformCallAsync<Todo>(restCall);
            var content = actualResponse.Content;

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.IsTrue(content.Id > 0);
            Assert.AreEqual(todo.Completed, content.Completed);
            Assert.AreEqual(todo.Title, content.Title);
            Assert.AreEqual(todo.UserId, content.UserId);
        }
    }
}