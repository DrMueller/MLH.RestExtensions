﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.RestExtensions.Areas.RestProxies;
using Mmu.Mlh.RestExtensions.IntegrationTests.TestingInfrastructure.Context;
using Mmu.Mlh.RestExtensions.IntegrationTests.TestingInfrastructure.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.IntegrationTests.TestingAreas.Areas.RestProxies.Services
{
    [TestFixture]
    public class RestProxyIntegrationTests
    {
        [Test]
        public async Task PerformingCall_WithCorrectUrl_FetchesData()
        {
            // Arrange
            var context = RestTestContextBuilder.Create();
            var sut = context.ServiceLocator.GetService<IRestProxy>();
            var restCall = DataGenerator.CreateGetOneTodoRestCall();

            // Act
            var actualResponse = await sut.SendAsync<Todo>(restCall);
            var content = actualResponse.Content;

            // Assert
            Assert.IsNotNull(content);
            Assert.AreEqual(1, content.Id);
            Assert.AreEqual(1, content.UserId);
            Assert.AreEqual("delectus aut autem", content.Title);
            Assert.AreEqual(false, content.Completed);
        }

        [Test]
        public async Task PerformingCall_WithQueryParameters_FetchesData_OfPostIdOne()
        {
            // Arrange
            var context = RestTestContextBuilder.Create();
            var sut = context.ServiceLocator.GetService<IRestProxy>();
            var restCall = DataGenerator.CreateCommentsByPostIdRestCall();

            // Act
            var actualResponse = await sut.SendAsync<List<Post>>(restCall);
            var content = actualResponse.Content;

            // Assert
            Assert.AreEqual(5, content.Count);
            Assert.IsTrue(content.All(f => f.PostId == 1));
        }

        [Test]
        public async Task PerformingGet_WithNotExistingUrl_ReturnsFailure()
        {
            // Arrange
            var context = RestTestContextBuilder.Create();
            var sut = context.ServiceLocator.GetService<IRestProxy>();
            var restCall = DataGenerator.CreateNotExistingGetRestCall();

            // Act
            var actualResponse = await sut.SendAsync(restCall);

            // Assert
            Assert.IsFalse(actualResponse.WasSuccess);
        }

        [Test]
        public async Task PerformingPost_WithApplicationJsonFromJsonString_SendsAsJson()
        {
            // Arrange
            var context = RestTestContextBuilder.Create();
            var sut = context.ServiceLocator.GetService<IRestProxy>();

            var todo = new Todo { Completed = true, Title = "Hello Test", UserId = 123 };
            var jsonString = JsonConvert.SerializeObject(todo);
            var restCall = DataGenerator.CreatePostOneTodoRestCall(jsonString);

            // Act
            var actualResponse = await sut.SendAsync<Todo>(restCall);
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
            var context = RestTestContextBuilder.Create();
            var sut = context.ServiceLocator.GetService<IRestProxy>();

            var todo = new Todo { Completed = true, Title = "Hello Test", UserId = 123 };
            var restCall = DataGenerator.CreatePostOneTodoRestCall(todo);

            // Act
            var actualResponse = await sut.SendAsync<Todo>(restCall);
            var content = actualResponse.Content;

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.IsTrue(content.Id > 0);
            Assert.AreEqual(todo.Completed, content.Completed);
            Assert.AreEqual(todo.Title, content.Title);
            Assert.AreEqual(todo.UserId, content.UserId);
        }

        [Test]
        public async Task PerformingPost_WithNotExistingUrl_ReturnsFailure()
        {
            // Arrange
            var context = RestTestContextBuilder.Create();
            var sut = context.ServiceLocator.GetService<IRestProxy>();
            var restCall = DataGenerator.CreateNotExistingPostRestCall();

            // Act
            var actualResponse = await sut.SendAsync<Todo>(restCall);

            // Assert
            Assert.AreEqual(405, actualResponse.StatusCode);
            Assert.IsNull(actualResponse.Content);
        }
    }
}