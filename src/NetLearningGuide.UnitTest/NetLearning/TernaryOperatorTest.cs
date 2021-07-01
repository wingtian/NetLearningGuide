using System.Collections.Generic;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning
{
    public class TernaryOperatorTest
    {
        [Fact]
        public Task TernaryOperatorTestCase1()
        {
            var model = new TestCase();
            var code = model.Code == null ? "" : model.Code;
            code.ShouldBe("");
            code = model.Code ?? "";
            code.ShouldBe("");
            return Task.CompletedTask;
        }
        [Fact]
        public Task TernaryOperatorTestCase2()
        {
            var list = new List<TestCaseTemp>();
            var model = new TestCase() { Code = "" };
            var modelTemp = list.FirstOrDefault();
            var code = modelTemp == null ? model.Code : modelTemp.Code;
            code.ShouldBe("");

            list.Add(new TestCaseTemp() { Code = "ABC" });
            modelTemp = list.FirstOrDefault();
            code = modelTemp == null ? model.Code : modelTemp.Code;
            code.ShouldBe("ABC");
            return Task.CompletedTask;
        }
        [Fact]
        public Task TernaryOperatorTestCase3()
        {
            var list = new List<TestCaseTemp>();
            var model = new TestCase() { Code = "" };
            var modelTemp = list.FirstOrDefault();
            var code = modelTemp?.Code ?? model.Code;
            code.ShouldBe("");

            list.Add(new TestCaseTemp() { Code = "ABC" });
            modelTemp = list.FirstOrDefault();
            code = modelTemp?.Code ?? model.Code;
            code.ShouldBe("ABC");
            return Task.CompletedTask;
        }
        private class TestCase
        {
            public string Code
            {
                get;
                set;
            }
        }

        private class TestCaseTemp
        {
            public string Code
            {
                get;
                set;
            }
        }
    }
}
