using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;
using Mmu.Mlh.RestExtensions.Areas.RestProxies;
using Mmu.Mlh.RestExtensions.BddTests.TestingInfrastructure.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.TestingExtensions.Areas.IntegrationTesting.Contexts.Builders;
using Mmu.Mlh.TestingExtensions.Areas.IntegrationTesting.Contexts.Models;
using NUnit.Framework;

namespace Mmu.Mlh.RestExtensions.BddTests.TestingAreas
{
    public partial class SendingRestCall
    {
        private const int PostId = 1;
        private readonly IIntegrationTestContext _integrationTestContext;
        private RestCall _restCall;
        private RestCallResult<List<Comment>> _fetchCommentsResult;

        public SendingRestCall()
        {
            var containerConfig = ContainerConfiguration.CreateFromAssembly(typeof(SendingRestCall).Assembly);

            _integrationTestContext = IntegrationTestContextBuilderFactory
                .StartBuilding(containerConfig)
                .Build();
        }

        public Task Given_the_user_is_about_to_call_a_nonexisting_url()
        {
            _restCall = new RestCall(
                new Uri("https://googlewdwwfwef.com/"),
                Maybe.CreateNone<string>(),
                RestCallMethodType.Post,
                RestSecurity.CreateAnonymous(),
                new RestHeaders(new List<RestHeader>()),
                Maybe.CreateNone<RestCallBody>(),
                new QueryParameters(new List<QueryParameter>()));

            return Task.CompletedTask;
        }

        public Task Given_the_user_is_about_to_fetch_comments_per_post_id()
        {
            var queryParameters = new List<QueryParameter> { new QueryParameter("postId", PostId) };

            _restCall = new RestCall(
                new Uri("https://jsonplaceholder.typicode.com/comments"),
                Maybe.CreateNone<string>(),
                RestCallMethodType.Get,
                RestSecurity.CreateAnonymous(),
                new RestHeaders(new List<RestHeader>()),
                Maybe.CreateNone<RestCallBody>(),
                new QueryParameters(queryParameters));

            return Task.CompletedTask;
        }

        public async Task When_the_user_sends_the_restcall()
        {
            var restProxy = _integrationTestContext.ServiceLocator.GetService<IRestProxy>();
            _fetchCommentsResult = await restProxy.SendAsync<List<Comment>>(_restCall);
        }

        public Task Then_the_call_was_sucessful()
        {
            Assert.IsTrue(_fetchCommentsResult.WasSuccess);
            return Task.CompletedTask;
        }

        public Task Then_the_call_was_not_sucessful()
        {
            Assert.False(_fetchCommentsResult.WasSuccess);
            return Task.CompletedTask;
        }

        public Task Then_the_result_content_contains_comments()
        {
            CollectionAssert.IsNotEmpty(_fetchCommentsResult.Content);
            return Task.CompletedTask;
        }

        public Task Then_the_result_content_contains_only_comments_with_the_passed_postid()
        {
            Assert.That(_fetchCommentsResult.Content.All(comment => comment.PostId == PostId));
            return Task.CompletedTask;
        }

        public Task Then_the_result_has_the_statusCode(int statusCode)
        {
            Assert.AreEqual(statusCode, _fetchCommentsResult.StatusCode);
            return Task.CompletedTask;
        }
    }
}