using Shouldly;
using System;
using System.Collections.Generic;
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

        [Fact]
        public Task DateTimeAddTestCase()
        { 
            var date = GetTargetDate(Convert.ToDateTime("2021-07-08"), new List<string>()
            {
                DayOfWeek.Saturday.ToString()
            }, 6);
            date.ToString("yyyy-MM-dd").ShouldBe("2021-07-15");
            var date2 = Convert.ToDateTime("2021-07-08").AddDays(6);
            date2.ToString("yyyy-MM-dd").ShouldBe("2021-07-14");
            var date3 = GetTargetDateFunc(Convert.ToDateTime("2021-07-08"), new List<string>()
            {
                DayOfWeek.Saturday.ToString()
            }, 9);
            date3.ToString("yyyy-MM-dd").ShouldBe("2021-07-18");
            return Task.CompletedTask;
        }

        private DateTime GetTargetDate(DateTime inputDate, List<string> skipDate, int addDate)
        {
            var result = inputDate;
            for (int i = 0; i < addDate; i++)
            {
                skipDate.ForEach(x =>
                {
                    if (result.DayOfWeek.ToString() == x)
                        result = result.AddDays(1);
                });
                result = result.AddDays(1);
            }
            return result;
        }
        private DateTime GetTargetDateFunc(DateTime inputDate, List<string> skipDate, int addDate)
        {
            var result = inputDate.AddDays(addDate);
            skipDate.ForEach(x =>
            {
                if (result.DayOfWeek.ToString() == x)
                    result = result.AddDays(1);
            });
            return result;
        }
    }
}
