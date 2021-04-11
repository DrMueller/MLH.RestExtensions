using System.Threading.Tasks;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace Mmu.Mlh.RestExtensionsSimple.BddTests.TestingAreas
{
    [FeatureDescription(
        @"In order to send a RestCall
As an user
I want to pass a RestCall")]
    public partial class SendingHttpCall
    {
        [Scenario]
        public async Task Calling_nonexisting_url()
        {
            await Runner.RunScenarioAsync(
                _ => Given_the_user_is_about_to_call_a_nonexisting_url(),
                _ => When_the_user_sends_the_restcall(),
                _ => Then_the_call_was_not_sucessful(),
                _ => Then_the_result_has_the_statusCode(500));
        }

        [Scenario]
        public async Task Successfully_fetching_existing_comments_by_post_id()
        {
            await Runner.RunScenarioAsync(
                Given_the_user_is_about_to_fetch_comments_per_post_id,
                When_the_user_sends_the_restcall,
                Then_the_call_was_sucessful,
                Then_the_result_content_contains_comments,
                Then_the_result_content_contains_only_comments_with_the_passed_postid);
        }
    }
}