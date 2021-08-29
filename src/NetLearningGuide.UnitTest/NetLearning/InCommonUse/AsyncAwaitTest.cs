using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class AsyncAwaitTest
    {
        #region  ForEach 异步和同步测试,Case1 特性最需要关注.
        [Fact]
        public Task AsyncAwaitForEachTestCase1()
        {
            var result = true;
            var myList = new List<string>() { "1", "2" };
            myList.ForEach(async x =>
            {
                await Task.Delay(100);
                result = false;
            });
            result.ShouldBeTrue();
            return Task.CompletedTask;
        }

        [Fact]
        public async Task AsyncAwaitForEachTestCase2()
        {
            var result = true;
            var myList = new List<string>() { "1", "2" };
            myList.ForEach(async x =>
            {
                await Task.Delay(100);
                result = false;
            });
            await Task.Delay(200);
            result.ShouldBeFalse();
        }

        [Fact]
        public Task AsyncAwaitForEachTestCase3()
        {
            var result = true;
            var myList = new List<string>() { "1", "2" };
            myList.ForEach(x =>
            {
                result = false;
            });
            result.ShouldBeFalse();
            return Task.CompletedTask;
        }
        #endregion

        #region Task.FromResult 
        private string ReturnHello()
        {
            return "Hello";
        }

        [Fact]
        public async Task FromResultTestCase1()
        {
            var result = await Task.FromResult(ReturnHello());
            result.ShouldBe("Hello");
        }
        #endregion
        #region Task.Run
        [Fact]
        public async Task RunTestCase1()
        {
            var result = await Task.Run(() => ReturnHello());
            result.ShouldBe("Hello");
        } 
        #endregion
    }
}
