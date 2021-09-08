﻿using System;
using System.Collections.Generic;
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
    }
}