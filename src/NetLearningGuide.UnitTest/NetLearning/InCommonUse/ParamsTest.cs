using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class ParamsTest
    {
        /// https://www.cnblogs.com/maowp/p/8134342.html
        [Fact]
        public Task ParamsTestCase1()
        {
            var input = new[] { 6, 5, 2, 7, 10, 20, 60, 4 };
            var result = ShowParamsMaxValue(2, 3);
            result.ShouldBe(3);
            result = ShowMaxValue(input);
            result.ShouldBe(60);
            return Task.CompletedTask;
        }

        private int ShowParamsMaxValue(params int[] arr)
        {
            int maxValue = 0;
            if (arr != null && arr.Length > 0)
            {
                Array.Sort(arr);
                maxValue = arr[^1];
            }
            return maxValue;
        }
        private int ShowMaxValue(int[] arr)
        {
            int maxValue = 0;
            if (arr != null && arr.Length > 0)
            {
                Array.Sort(arr);
                maxValue = arr[^1];
            }
            return maxValue;
        }
    }
}
