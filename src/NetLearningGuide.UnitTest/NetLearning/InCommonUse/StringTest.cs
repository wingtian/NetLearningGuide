using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class StringTest
    {
        [Fact]
        public Task TrimStringTest()
        {
            string test = "0000123";
            test = test.TrimStart('0');
            test.ShouldBe("123");
            return Task.CompletedTask;
        }
    }
}
