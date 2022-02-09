using System;
using System.Collections.Generic;
using Shouldly;
using System.Diagnostics;
using System.Reflection;
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

        [Fact]
        public async Task TimeZoneTestCase1()
        {
            //var local = TimeZoneInfo.Local;
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            foreach (var item in timeZones)
            {
                Console.WriteLine("{0} - {1}", item.Id, item.StandardName);
            }
            //-4 Atlantic Standard Time
            var offset = TimeZoneInfo.FindSystemTimeZoneById("UTC").BaseUtcOffset;
            var date1 = new DateTimeOffset(new DateTime(2021, 5, 1), offset);
            var test1 = GetRealPacificStandardTimeOfSet(date1);
            test1.ShouldBe(-8);
            var date2 = new DateTimeOffset(new DateTime(2021, 12, 1), offset);
            var test2 = GetRealPacificStandardTimeOfSet(date2);
            test2.ShouldBe(-7);
            var test3 = new TimeSpan(test2, 0, 0);
        }

        [Fact]
        public async Task TimeZoneTestCase2()
        {
            var date1 = new DateTimeOffset(new DateTime(2021, 5, 1), new TimeSpan(-8, 0, 0));
            var date = date1.UtcDateTime;
            var datetime1 = date1.DateTime.AddHours(-date1.Offset.Hours);
            var date2 = new DateTimeOffset(datetime1, new TimeSpan(-2, 0, 0));
        }
        [Fact]
        public async Task TimeZoneTestCase3()
        {
            var date1 = DateTimeOffset.Now;
            var test1 = date1.ToOffset(new TimeSpan(-8, 0, 0));
            var test2 = date1.ToOffset(new TimeSpan(0, 0, 0));
            var test3 = date1.ToOffset(new TimeSpan(8, 0, 0));

            var date2 = new DateTimeOffset(new DateTime(2021, 5, 1), new TimeSpan(-8, 0, 0));
            var datetime2 = date2.DateTime.AddHours(-date1.Offset.Hours);
            var date3 = date2.ToOffset(new TimeSpan(3, 0, 0));
            var datetime3 = date2.DateTime.AddHours(date3.Offset.Hours - date1.Offset.Hours);
        }
        [Fact]
        public async Task TimeZoneTestCase4()
        {
            var offset = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time11").BaseUtcOffset;
        }
        private int GetRealPacificStandardTimeOfSet(DateTimeOffset input)
        {
            var offset = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time").BaseUtcOffset;
            return TimeZoneInfo.Local.IsDaylightSavingTime(input) ? offset.Hours : offset.Hours + 1;
        }
        [Fact]
        public async Task AttributeTestCase1()
        {
            DateTimeOffset aaa = DateTime.Now;
            DateTime bbb = DateTimeOffset.Now.DateTime;
        }
    }
}
