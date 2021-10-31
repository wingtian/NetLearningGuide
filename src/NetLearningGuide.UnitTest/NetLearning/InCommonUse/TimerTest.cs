using Shouldly;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class TimerTest
    {
        [Fact]
        public async Task FromResultTestCase1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await Task.Delay(1000);
            sw.Stop();
            var time = sw.ElapsedTicks / (decimal)Stopwatch.Frequency;
            time.ShouldBeGreaterThanOrEqualTo(1);
        }
    }
}
