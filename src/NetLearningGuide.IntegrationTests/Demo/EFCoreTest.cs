using System;
using System.Threading.Tasks;
using Mediator.Net;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Requests.Demo;
using Shouldly;
using Xunit;

namespace NetLearningGuide.IntegrationTests.Demo
{
    public class EfCoreTest : GeneralTestFixtureBase
    {
        [Fact]
        public async Task EfCoreCurdTest()
        {
            var guid = Guid.NewGuid();
            var deleteCommand = new DemoEfDeleteCommand(guid);
            var insertCommand = new DemoEfInsertCommand(guid);
            var updateCommand = new DemoEfUpdateCommand(guid);
            var queryRequest = new DemoEfQueryRequest(guid);
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoEfDeleteCommand, CommonResponse<bool>>(deleteCommand);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                result.Data.ShouldBeFalse();

                result = await mediator.SendAsync<DemoEfInsertCommand, CommonResponse<bool>>(insertCommand);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                result.Data.ShouldBeTrue();

                var resultQuery = await mediator.RequestAsync<DemoEfQueryRequest, CommonResponse<TestDbUp>>(queryRequest);
                resultQuery.Code.ShouldBe(200);
                resultQuery.Message.ShouldBe("OK");
                resultQuery.Data.Guid.ShouldBe(guid);
                resultQuery.Data.DescInfo.ShouldBe("添加測試");

                result = await mediator.SendAsync<DemoEfUpdateCommand, CommonResponse<bool>>(updateCommand);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                result.Data.ShouldBeTrue();

                resultQuery = await mediator.RequestAsync<DemoEfQueryRequest, CommonResponse<TestDbUp>>(queryRequest);
                resultQuery.Code.ShouldBe(200);
                resultQuery.Message.ShouldBe("OK");
                resultQuery.Data.Guid.ShouldBe(guid);
                resultQuery.Data.DescInfo.ShouldBe("更新測試");

                result = await mediator.SendAsync<DemoEfDeleteCommand, CommonResponse<bool>>(deleteCommand);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                result.Data.ShouldBeTrue();

                resultQuery = await mediator.RequestAsync<DemoEfQueryRequest, CommonResponse<TestDbUp>>(queryRequest);
                resultQuery.Code.ShouldBe(200);
                resultQuery.Message.ShouldBe("OK");
                resultQuery.Data.ShouldBeNull();
            });
        }
    }
}
