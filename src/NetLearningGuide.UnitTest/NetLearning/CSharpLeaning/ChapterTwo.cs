using System.Globalization;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.CSharpLeaning
{
    public class ChapterTwo
    {
        /// <summary>
        /// Double 是二进制浮点类型,有效数字是15到16位,Double有精度问题参考下面DoubleTestCase2这个测试
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task DoubleTestCase1()
        {
            var doubleValue = 1.61803398874989511;
            doubleValue.ShouldBe(1.6180339887498951);
            return Task.CompletedTask;
        }
        [Fact]
        public Task DoubleTestCase2()
        {
            var doubleValue = 1.618033988749895;
            doubleValue.ShouldBe(1.6180339887498949);
            return Task.CompletedTask;
        }
        /// <summary>
        /// Decimal 是十进制浮点类型,对于所有十进制的数都是精确的,有效数字28到29位,常用于货币计算.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task DecimalTestCase1()
        {
            var decimalValue = 1.618033988749895111m;
            decimalValue.ShouldBe(1.618033988749895111m);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 指数计数法
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task ExponentialCountingMethodCase1()
        {
            float value = 6.023E23F;//6.023 * 10^23
            value.ToString(CultureInfo.InvariantCulture).ShouldBe("6.023E+23");
            value = 1.1E2F;//1.1 * 10^2
            value.ToString(CultureInfo.InvariantCulture).ShouldBe("110");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 将数格式化成十六进制
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task FormattedInHexadecimalCase1()
        {
            var value = $"0x{42:X}";
            value.ShouldBe("0x2A");
            return Task.CompletedTask;
        }

        /// <summary>
        /// round-trip 格式化
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task RoundTripCase1()
        {
            const double number = 1.618033988749895;  //1.6180339887498949

            var text = $"{number}";
            var result = double.Parse(text);          //1.6180339887498949
            result.ShouldBe(number);

            text = $"{number:R}";                     //1.618033988749895
            result = double.Parse(text);              //1.6180339887498949
            result.ShouldBe(number);
            return Task.CompletedTask;
        }
    }
}
