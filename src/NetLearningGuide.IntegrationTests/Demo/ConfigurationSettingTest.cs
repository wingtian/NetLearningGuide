using Mediator.Net;
using Microsoft.Extensions.Configuration;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Requests.Demo;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.IntegrationTests.Demo
{
    public class ConfigurationSettingTest : TestBase
    {
        [Fact]
        public async Task ConfigurationTest()
        {
            await Run<IMediator>(async mediator =>
                {
                    var request = await mediator.RequestAsync<DemoConfigurationRequest, CommonResponse<string>>(
                         new DemoConfigurationRequest()).ConfigureAwait(false);
                    request.Data.ShouldBe(Configuration.GetValue<string>("JWT:Audience"));
                    request.Code.ShouldBe(200);
                    request.Message.ShouldBe("OK");
                });
        }
    }
}
