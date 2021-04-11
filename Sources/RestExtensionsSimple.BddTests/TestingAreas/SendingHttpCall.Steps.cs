using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Lamar;
using LightBDD.XUnit2;
using Mmu.Mlh.RestExtensionsSimple.Areas.Models;
using Mmu.Mlh.RestExtensionsSimple.Areas.Services;
using Mmu.Mlh.RestExtensionsSimple.BddTests.TestingInfrastructure.DependencyInjection;
using Mmu.Mlh.RestExtensionsSimple.BddTests.TestingInfrastructure.Models;
using Xunit;

namespace Mmu.Mlh.RestExtensionsSimple.BddTests.TestingAreas
{
    public partial class SendingHttpCall : FeatureFixture
    {
        private const int PostId = 1;
        private readonly IContainer _container;
        private HttpCallResult<List<Comment>> _fetchCommentsResult;
        private HttpCall _restCall;

        public SendingHttpCall()
        {
            _container = TestContainerFactory.Create();
        }

        private Task Given_the_user_is_about_to_call_a_nonexisting_url()
        {
            _restCall = new HttpCall(
                "https://googlewdwwfwef.com/",
                HttpCallMethodType.Post,
                new BasicAuthCredentials("Tra", "Tra"));

            return Task.CompletedTask;
        }

        private Task Given_the_user_is_about_to_fetch_comments_per_post_id()
        {
            var url = $"https://jsonplaceholder.typicode.com/comments?postId={PostId}";

            _restCall = new HttpCall(
                url,
                HttpCallMethodType.Get,
                new BasicAuthCredentials("Tra", "Tra"));

            return Task.CompletedTask;
        }

        private Task Then_the_call_was_not_sucessful()
        {
            Assert.False(_fetchCommentsResult.WasSuccess);
            return Task.CompletedTask;
        }

        private Task Then_the_call_was_sucessful()
        {
            _fetchCommentsResult.WasSuccess.Should().BeTrue();
            return Task.CompletedTask;
        }

        private Task Then_the_result_content_contains_comments()
        {
            _fetchCommentsResult.Result.Should().NotBeEmpty();
            return Task.CompletedTask;
        }

        private Task Then_the_result_content_contains_only_comments_with_the_passed_postid()
        {
            _fetchCommentsResult.Result.Should().OnlyContain(comment => comment.PostId == PostId);
            return Task.CompletedTask;
        }

        private Task Then_the_result_has_the_statusCode(int statusCode)
        {
            _fetchCommentsResult.StatusCode.Should().Be(statusCode);
            return Task.CompletedTask;
        }

        private async Task When_the_user_sends_the_restcall()
        {
            var clientProxyFactory = _container.GetInstance<IHttpClientProxyFactory>();
            var clientProxy = clientProxyFactory.Create();
            _fetchCommentsResult = await clientProxy.SendAsync<List<Comment>>(_restCall);
        }
    }
}