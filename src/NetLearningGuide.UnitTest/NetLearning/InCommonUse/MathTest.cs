using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class MathTest
    {
        [Fact]
        public Task MathRoundTestCase1()
        {
            decimal test = Math.Round(1.1m, 2);
            test.ToString().ShouldBe("1.1");
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathRoundTestCase2()
        {
            decimal test = Math.Round(1.1m, 2);
            test.ToString("#0.00").ShouldBe("1.10");
            var test1 = Convert.ToSingle(test.ToString("#0.00"));
            var test2 = float.Parse(test.ToString("#0.00"));
            var test3 = 1.10f;
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathRoundTestCase3()
        {
            decimal test = Math.Round(1.1m, 0);
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("1");
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathRoundTestCase4()
        {
            decimal test = Math.Round(1.199m, 2);
            test.ToString("#0.00").ShouldBe("1.20");
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathFloorTestCase1()
        {
            var test = Convert.ToInt16(Math.Floor(1.1m));
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("1");
            test = Convert.ToInt16(Math.Floor(1.9m));
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("1");
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathCeilingTestCase1()
        {
            var test = Convert.ToInt16(Math.Ceiling(1.1m));
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("2");
            test = Convert.ToInt16(Math.Ceiling(1.9m));
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("2");
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathAbsTestCase1()
        {
            var test = Math.Abs(-1.1m);
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("1.1");
            test = Math.Abs(1.9m);
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("1.9");
            return Task.CompletedTask;
        }
        //Math.acos()用于计算反余弦，返回值的单位为弧度，对于[-1,1]之间的元素，函数值域为[0,pi]，
        [Fact]
        public Task MathAcosTestCase1()
        {
            var test = Math.Acos(-1.1d);
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("NaN");
            test = Math.Acos(1.9d);
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("NaN");
            test = Math.Acos(0.32d);
            test.ToString(CultureInfo.InvariantCulture).ShouldBe("1.2450668395002664");
            return Task.CompletedTask;
        }

        [Fact]
        public Task MathETestCase1()
        {
            var e = Math.E;
            e.ToString(CultureInfo.InvariantCulture).ShouldBe("2.718281828459045");
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathPiTestCase1()
        {
            var e = Math.PI;
            e.ToString(CultureInfo.InvariantCulture).ShouldBe("3.141592653589793");
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathTauTestCase1()
        {
            var e = Math.Tau;
            e.ToString(CultureInfo.InvariantCulture).ShouldBe("6.283185307179586");
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathMaxCase1()
        {
            var e = Math.Max(1, 2);
            e.ShouldBe(2);
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathMinCase1()
        {
            var e = Math.Min(1, 2);
            e.ShouldBe(1);
            return Task.CompletedTask;
        }

        [Fact]
        public Task DivideTestCase1()
        {
            int a = 1;
            int b = 100;
            var c = Convert.ToDecimal(a) / Convert.ToDecimal(b);
            var d = a / b;
            c.ShouldBe(0.01m);
            d.ShouldBe(0);
            return Task.CompletedTask;
        }

        [Fact]
        public Task MathCosTestCase1()
        {
            var test = Math.Cos(60 * Math.PI / 180);
            test.ShouldBe(0.50000000000000011d);
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathSinTestCase1()
        {
            var test = Math.Sin(30 * Math.PI / 180);
            test.ShouldBe(0.49999999999999994d);
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathTanTestCase1()
        {
            var test = Math.Tan(45 * Math.PI / 180);
            test.ShouldBe(0.99999999999999989d); 
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathAtanTestCase1()
        {
            var test = Math.Atan(45 * Math.PI / 180);
            test.ShouldBe(0.66577375002835382d); 
            return Task.CompletedTask;
        }
        [Fact]
        public Task MathAsinTestCase1()
        {
            var test = Math.Asin(30 * Math.PI / 180);
            test.ShouldBe(0.55106958309944631d);
            return Task.CompletedTask;
        }
    }
}
