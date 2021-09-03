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

        [Fact]
        public Task DateTimeTestCase1()
        {
            var test = DateTime.MinValue.DayOfWeek;
            test.ShouldBe(DayOfWeek.Monday);
            var date = new TestDateTime();
            var result1 = date.Time1 > date.Time2;
            (result1).ShouldBe(false);
            var result2 = date.Time1 == date.Time2;
            (result2).ShouldBe(false);
            var result3 = date.Time1 < date.Time2;
            (result3).ShouldBe(false);
            date.Time1.ShouldBe(DateTime.MinValue);
            date.Time1 = DateTime.Now;
            (date.Time1 > date.Time2).ShouldBe(false);
            (date.Time1 < date.Time2).ShouldBe(false);
            (date.Time1 == date.Time2).ShouldBe(false);
            return Task.CompletedTask;
        }
        private class TestDateTime
        {
            public DateTime Time1 { get; set; }
            public DateTime? Time2 { get; set; }
        }

        [Fact]
        public Task DateDiffSkipSundayTestCase1()
        {
            var startDate = Convert.ToDateTime("2021-09-04");
            var endDate = Convert.ToDateTime("2021-09-06");
            var result = DateDiffSkipSunday(startDate, endDate);
            result.ShouldBe(2);
            return Task.CompletedTask;
        }

        private int DateDiffSkipSunday(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());
            var sp = end.Subtract(start).Days;
            var count = sp;
            for (int i = 1; i <= count; i++)
            {
                if (start.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                    sp--;
            }
            return sp + 1;
        }
    }
}
