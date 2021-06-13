using System;
using System.Threading.Tasks;
using Mediator.Net;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;
using NetLearningGuide.Message.Events.Demo;
using NetLearningGuide.Message.Requests.Demo;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.Demo
{
    public class MiddlewaresTest : DemoBase
    {
        #region SendAsync Test
        [Fact]
        public async Task UnifyResponseMiddlewareSpecificationOnExceptionSendTest()
        {
            try
            {
                var command = new DemoExceptionCommand(new Guid());
                await Run<IMediator>(async mediator =>
                {
                    await mediator.SendAsync<DemoExceptionCommand, CommonResponse<DemoDto>>(command);
                });
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe("System Exception");
            }
        }
        [Fact]
        public async Task UnifyResponseMiddlewareSpecificationOnBusinessExceptionSendTest()
        {
            var command = new DemoBusinessExceptionCommand(new Guid());
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoBusinessExceptionCommand, CommonResponse<DemoDto>>(command);
                result.Message.ShouldBe("BusinessException");
                result.Code.ShouldBe(500);
            });
        }
        [Fact]
        public async Task UnifyResponseMiddlewareSpecificationOnSendTest()
        {
            var command = new DemoCommand(new Guid());
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoCommand, CommonResponse<DemoDto>>(command);
                result.Message.ShouldBe("OK");
                result.Data.Id.ShouldNotBeNull();
                result.Code.ShouldBe(200);
            });
        }
        #endregion

        #region PublishAsync Test
        [Fact]
        public async Task UnifyResponseMiddlewareSpecificationOnExceptionPublishTest()
        {
            try
            {
                var command = new DemoExceptionEvent(new Guid());
                await Run<IMediator>(async mediator =>
                {
                    await mediator.PublishAsync(command);
                });
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe("System Exception");
            }
        }
        [Fact]
        public async Task UnifyResponseMiddlewareSpecificationOnBusinessExceptionPublishTest()
        {
            try
            {
                var command = new DemoBusinessExceptionEvent(new Guid());
                await Run<IMediator>(async mediator =>
                {
                    await mediator.PublishAsync(command);
                });
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe("BusinessException");
            }
        }
        [Fact]
        public async Task UnifyResponseMiddlewareSpecificationOnPublishTest()
        {
            var command = new DemoEvent(new Guid());
            await Run<IMediator>(async mediator =>
            {
                await mediator.PublishAsync(command);
            });
        }
        #endregion
        #region RequestAsync Test
        [Fact]
        public async Task UnifyResponseMiddlewareSpecificationOnExceptionRequestTest()
        {
            var command = new DemoExceptionRequest();
            try
            {
                await Run<IMediator>(async mediator =>
                {
                    await mediator.RequestAsync<DemoExceptionRequest, CommonResponse<DemoDto>>(command);
                });
            }
            catch (Exception ex)
            {
                ex.Message.ShouldBe("System Exception");
            }
        }
        [Fact]
        public async Task UnifyResponseMiddlewareSpecificationOnBusinessExceptionRequestTest()
        {
            var command = new DemoBusinessExceptionRequest();
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.RequestAsync<DemoBusinessExceptionRequest, CommonResponse<DemoDto>>(command);
                result.Message.ShouldBe("BusinessException");
                result.Code.ShouldBe(500);
            });
        }
        [Fact]
        public async Task UnifyResponseMiddlewareSpecificationOnRequestTest()
        {
            var command = new DemoRequest();
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.RequestAsync<DemoRequest, CommonResponse<DemoDto>>(command);
                result.Message.ShouldBe("OK");
                result.Data.Id.ShouldNotBeNull();
                result.Code.ShouldBe(200);
            });
        }
        #endregion 
    }
}
