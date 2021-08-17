using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetLearningGuide.Core.HttpClientHelper;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class EnumTest
    {
        ///https://www.cnblogs.com/salmol/p/14720130.html
        private enum Db
        {
            A,
            B,
            C,
            D,
        }
        [Fact]
        public Task GetEnumName()
        {
            var list = new List<string>() { "A", "B", "C", "D" };
            foreach (var db in Enum.GetNames(typeof(Db)))
            {
                list.ShouldContain(db);
            }
            return Task.CompletedTask;
        }
        [Fact]
        public Task GetEnumValue()
        {
            var list = new List<int>() { 0, 1, 2, 3 };
            foreach (var db in Enum.GetValues(typeof(Db)))
            {
                list.ShouldContain((int)db);
            }
            return Task.CompletedTask;
        }

        [Fact]
        public Task DictionaryForLoopTestCase()
        {
            try
            {

                var inModel = new HttpTestModel();

                foreach (var item in inModel?.Headers)
                {

                }
            }
            catch (Exception e)
            {
                e.Message.ShouldBe("Object reference not set to an instance of an object.");
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// 提供常用的 Url屬性
        /// </summary>
        public class HttpTestModel
        {
            public string BaseUrl { get; set; }
            /// <summary>
            /// 默認請求頭
            /// </summary>
            public Dictionary<string, string> Headers { get; set; }

            public TokenModel Token { get; set; }
        }

        [Flags]
        enum Week
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 4,
            Thursday = 8,
            Friday = 16,
            Saturday = 32,
            Sunday = 64,
        }

        [Fact]
        public Task EnumFlagsTestCase1()
        {
            int test = (int)(Week.Monday | Week.Tuesday);
            test.ShouldBe(3);
            var test2 = (Week)Enum.Parse(typeof(Week), "3");
            test2.ShouldBe(Week.Monday | Week.Tuesday);
            var test3 = (Week)Enum.Parse(typeof(Week), "127");
            test3.ShouldBe(Week.Monday | Week.Tuesday | Week.Wednesday | Week.Thursday | Week.Friday | Week.Saturday | Week.Sunday);
            Week test4 = (Week)Enum.Parse(typeof(Week), "0");
            test4.ShouldBe((Week)(0));
            return Task.CompletedTask;
        }
    }
}
