using System.Text.RegularExpressions;
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

        [Fact]
        public Task FormatStringTest()
        {
            string test = "0000123PDC";
            test = FormatString("0000123");
            test.ShouldBe("123");
            return Task.CompletedTask;
        }

        private string FormatString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            string outPut = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (IsInt(input.Substring(i, 1)))
                    outPut += input.Substring(i, 1);
            }
            return outPut.TrimStart('0');
        }
        private static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
    }
}
