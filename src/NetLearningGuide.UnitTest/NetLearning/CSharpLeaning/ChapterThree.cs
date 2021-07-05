using System;
using System.Globalization;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.CSharpLeaning
{
    public class ChapterThree
    {
        [Fact]
        public Task BinaryOperatorCase1()
        {
            int numerator = 23;
            int denominator = 3;
            (numerator / denominator).ShouldBe(7);
            (numerator % denominator).ShouldBe(2);
            return Task.CompletedTask;
        }

        [Fact]
        public Task CharOperatorCase1()
        {
            int t = '3';
            t.ShouldBe(51);
            int y = '4';
            y.ShouldBe(52);
            int n = '3' + '4';
            n.ShouldBe(103);
            char c = (char)n;
            c.ToString().ShouldBe("g");
            return Task.CompletedTask;
        }

        [Fact]
        public Task CharOperatorCase2()
        {
            int t = 'f' - 'c';
            t.ShouldBe(3);
            return Task.CompletedTask;
        }


        /// <summary>
        /// 下面例子可以看出，计算还是用decimal比较合适。  double 和float都会有精度问题
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task FloatingPointTypeOperatorCase1()
        {
            decimal decimalNumber = 4.2m;
            double doubleNumber1 = 0.1F * 42F;
            double doubleNumber2 = 0.1D * 42D;
            float floatNumber = 0.1F * 42F;

            (decimalNumber != (decimal)doubleNumber1).ShouldBeTrue();
            (Math.Abs((double)decimalNumber - doubleNumber1) > 0).ShouldBeTrue(); //差值不为0
            (Math.Abs((float)decimalNumber - floatNumber) > 0).ShouldBeTrue();//差值不为0
            (Math.Abs(doubleNumber1 - floatNumber) < 1).ShouldBeTrue();// 差值为0
            (Math.Abs(doubleNumber1 - doubleNumber2) > 0).ShouldBeTrue();//差值不为0
            (Math.Abs(floatNumber - doubleNumber2) > 0).ShouldBeTrue();//差值不为0
            (Math.Abs(4.2F - 4.2D) > 0).ShouldBeTrue();//差值不为0
            (Math.Abs(4.2F - 4.2D) > 0).ShouldBeTrue();//差值不为0
            return Task.CompletedTask;
        }

        [Fact]
        public Task NotANumberTaskCase1()
        {
            float n = 0f;
            (n / 0).ToString(CultureInfo.InvariantCulture).ShouldBe("NaN");
            (-1f / 0).ToString(CultureInfo.InvariantCulture).ShouldBe("-Infinity");
            (3.402823E+38f * 2f).ToString(CultureInfo.InvariantCulture).ShouldBe("Infinity");
            return Task.CompletedTask;
        }

        [Fact]
        public Task CompoundAssignmentCase1()
        {
            int x = 123;
            x = x + 2;
            x.ShouldBe(125);
            return Task.CompletedTask;
        }
        [Fact]
        public Task CompoundAssignmentCase2()
        {
            int x = 123;
            x += 2;
            x.ShouldBe(125);
            x -= 2;
            x.ShouldBe(123);
            x /= 2;
            x.ShouldBe(61);
            x *= 2;
            x.ShouldBe(122);
            x %= 2;
            x.ShouldBe(0);
            return Task.CompletedTask;
        }
        [Fact]
        public Task CompoundAssignmentCase3()
        {
            int x = 123;
            x = x + 2;
            x.ShouldBe(125);
            x += 1;
            x.ShouldBe(126);
            x++;
            x.ShouldBe(127);
            x -= 1;
            x.ShouldBe(126);
            x--;
            x.ShouldBe(125);
            return Task.CompletedTask;
        }

        [Fact]
        public Task CompoundAssignmentCase4()
        {
            int count = 123;
            int result = count++;
            result.ShouldBe(123);
            count.ShouldBe(124);
            return Task.CompletedTask;
        }
        [Fact]
        public Task CompoundAssignmentCase5()
        {
            int count = 123;
            int result = ++count;
            result.ShouldBe(124);
            count.ShouldBe(124);
            return Task.CompletedTask;
        }
        [Fact]
        public Task CompoundAssignmentCase6()
        {
            int x = 123;
            (x++).ShouldBe(123);
            (x++).ShouldBe(124);
            (x).ShouldBe(125);
            (++x).ShouldBe(126);
            (++x).ShouldBe(127);
            (x).ShouldBe(127);
            return Task.CompletedTask;
        }
    }
}
