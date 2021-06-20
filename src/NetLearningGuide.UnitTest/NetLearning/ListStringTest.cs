using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning
{
    public class ListStringTest
    {
        #region List<string>交集、補集、超集、串聯 https://www.cnblogs.com/keepee/p/9963332.html
        [Fact]
        public Task ListStringIntersectTest()
        {
            List<string> ls1 = new List<string> { "a", "b", "c", "d" };

            List<string> ls2 = new List<string> { "a", "c", "d", "e" };

            var result = ls1.Intersect(ls2).ToList();
            result.ShouldContain("a");
            result.ShouldContain("c");
            result.ShouldContain("d");
            return Task.CompletedTask;
        }

        [Fact]
        public Task ListStringExceptTest()
        {
            List<string> ls1 = new List<string> { "a", "b", "c", "d" };

            List<string> ls2 = new List<string> { "a", "c", "d", "e" };

            var result = ls1.Except(ls2).ToList();
            result.ShouldContain("b");
            return Task.CompletedTask;
        }

        [Fact]
        public Task ListStringUnionTest()
        {
            List<string> ls1 = new List<string> { "a", "b", "c", "d" };

            List<string> ls2 = new List<string> { "a", "c", "d", "e" };

            var result = ls1.Union(ls2).ToList();
            result.ShouldContain("a");
            result.ShouldContain("b");
            result.ShouldContain("c");
            result.ShouldContain("d");
            result.ShouldContain("e");
            return Task.CompletedTask;
        }

        [Fact]
        public Task ListStringConcatTest()
        {
            List<string> ls1 = new List<string> { "a", "b", "c", "d" };

            List<string> ls2 = new List<string> { "a", "c", "d", "e" };

            var result = ls1.Concat(ls2).ToList();
            result.ShouldContain("a");
            result.ShouldContain("b");
            result.ShouldContain("c");
            result.ShouldContain("d");
            result.ShouldContain("e");
            result.Count(x => x == "a").ShouldBe(2);
            result.Count(x => x == "c").ShouldBe(2);
            return Task.CompletedTask;
        }
        #endregion
    }
}
