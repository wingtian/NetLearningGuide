using Mediator.Net;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.Demo
{
    public class AutofacTest : DemoBase
    {
        #region AutoFac生命周期研究
        #region InstancePerLifetimeScope:同一个Lifetime生成的对象是同一个实例.
        [Fact]
        public async Task AutofacInstancePerLifetimeAsTheSameLifeTimeTest()
        {
            var command = new DemoAutofacInstancePerLifetimeCommand();
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoAutofacInstancePerLifetimeCommand, CommonResponse<string>>(command);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                var temp = result.Data;
                var resultAgain = await mediator.SendAsync<DemoAutofacInstancePerLifetimeCommand, CommonResponse<string>>(command);
                resultAgain.Code.ShouldBe(200);
                resultAgain.Message.ShouldBe("OK");
                resultAgain.Data.ShouldBe(temp);
            });
        }
        [Fact]
        public async Task AutofacInstancePerLifetimeInDifferentLiftTimeTest()
        {
            var command = new DemoAutofacInstancePerLifetimeCommand();
            var temp = "";
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoAutofacInstancePerLifetimeCommand, CommonResponse<string>>(command);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                temp = result.Data;
            });
            await Run<IMediator>(async mediator =>
            {
                var resultAgain = await mediator.SendAsync<DemoAutofacInstancePerLifetimeCommand, CommonResponse<string>>(command);
                resultAgain.Code.ShouldBe(200);
                resultAgain.Message.ShouldBe("OK");
                resultAgain.Data.ShouldNotBe(temp);
            });
        }
        #endregion
        #region SingleInstance:单例模式，每次调用，都会使用同一个实例化的对象,每次都用同一个对象.
        [Fact]
        public async Task AutofacSingletonAsTheSameLifeTimeTest()
        {
            var command = new DemoAutofacSingletonCommand();
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoAutofacSingletonCommand, CommonResponse<string>>(command);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                var temp = result.Data;
                var resultAgain = await mediator.SendAsync<DemoAutofacSingletonCommand, CommonResponse<string>>(command);
                resultAgain.Code.ShouldBe(200);
                resultAgain.Message.ShouldBe("OK");
                resultAgain.Data.ShouldBe(temp);
            });
        }
        [Fact]
        public async Task AutofacSingletonInDifferentLiftTimeTest()
        {
            var command = new DemoAutofacSingletonCommand();
            var temp = "";
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoAutofacSingletonCommand, CommonResponse<string>>(command);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                temp = result.Data;
            });
            await Run<IMediator>(async mediator =>
            {
                var resultAgain = await mediator.SendAsync<DemoAutofacSingletonCommand, CommonResponse<string>>(command);
                resultAgain.Code.ShouldBe(200);
                resultAgain.Message.ShouldBe("OK");
                resultAgain.Data.ShouldBe(temp);
            });
        }
        #endregion
        #region InstancePerDependency:默认模式，每次调用，都会重新实例化对象,每次请求都创建一个新的对象.
        [Fact]
        public async Task AutofacInstancePerDependencyAsTheSameLifeTimeTest()
        {
            var command = new DemoAutofacInstancePerDependencyCommand();
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoAutofacInstancePerDependencyCommand, CommonResponse<string>>(command);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                var temp = result.Data;
                var resultAgain = await mediator.SendAsync<DemoAutofacInstancePerDependencyCommand, CommonResponse<string>>(command);
                resultAgain.Code.ShouldBe(200);
                resultAgain.Message.ShouldBe("OK");
                resultAgain.Data.ShouldNotBe(temp);
            });
        }
        [Fact]
        public async Task AutofacInstancePerDependencyInDifferentLiftTimeTest()
        {
            var command = new DemoAutofacInstancePerDependencyCommand();
            var temp = "";
            await Run<IMediator>(async mediator =>
            {
                var result = await mediator.SendAsync<DemoAutofacInstancePerDependencyCommand, CommonResponse<string>>(command);
                result.Code.ShouldBe(200);
                result.Message.ShouldBe("OK");
                temp = result.Data;
            });
            await Run<IMediator>(async mediator =>
            {
                var resultAgain = await mediator.SendAsync<DemoAutofacInstancePerDependencyCommand, CommonResponse<string>>(command);
                resultAgain.Code.ShouldBe(200);
                resultAgain.Message.ShouldBe("OK");
                resultAgain.Data.ShouldNotBe(temp);
            });
        }
        #endregion
        #endregion
    }
}
