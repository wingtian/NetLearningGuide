using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetLearningGuide.Core.HttpClientHelper;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class EnumForeachTest
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
    }
}
