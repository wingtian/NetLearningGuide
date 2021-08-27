using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class DateTimeTest
    {
        [Fact]
        public Task DateTimeSubtractTestCase()
        {
            var test1 = DateDiff(Convert.ToDateTime("2021-07-22"), Convert.ToDateTime("2021-07-28"));
            var test2 = DateDiff(Convert.ToDateTime("2021-07-15"), Convert.ToDateTime("2021-07-28"));
            test1.ShouldBe(6);
            test2.ShouldBe(13);
            return Task.CompletedTask;
        }
        public int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());
            TimeSpan sp = end.Subtract(start);
            return sp.Days;
        }
    }
}
