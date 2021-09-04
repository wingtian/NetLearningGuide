using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class QuestionMarkTest
    {
        [Fact]
        public Task NewClassTestCase1()
        {
            IntTestClass test = null;
            int result = test?.Input ?? 2;
            result.ShouldBe(2);
            return Task.CompletedTask;
        }
        public class IntTestClass
        {
            public int Input { get; set; }
        }
    }
}
