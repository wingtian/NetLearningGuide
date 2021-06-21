using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning
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
    }
}
