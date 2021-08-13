using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class SwitchTest
    {
        [Fact]
        public Task SwitchTestCase1()
        {
            TestFunction(0).ShouldBeFalse();
            TestFunction(1).ShouldBeTrue();
            TestFunction(2).ShouldBeFalse();
            TestFunction(3).ShouldBeFalse();
            return Task.CompletedTask;
        }

        private bool TestFunction(int input)
        {
            return input switch
            {
                0 => false,
                1 => true,
                2 => false,
                _ => false,
            };
        }
        [Fact]
        public Task SwitchTestCase2()
        {
            TestFunctionCase2(0,new TestSwitchClass(true)).ShouldBeFalse();
            TestFunctionCase2(0, new TestSwitchClass(false)).ShouldBeTrue();
            return Task.CompletedTask;
        }
        private bool TestFunctionCase2(int input, TestSwitchClass ts)
        {
            return input switch
            {
                0 when ts.IsTrue => !ts.IsTrue,
                0 when !ts.IsTrue => !ts.IsTrue,
                _ => false,
            };
        }

        public class TestSwitchClass
        {
            public TestSwitchClass(bool isTrue)
            {
                IsTrue = isTrue;
            }
            public bool IsTrue { get; set; }
        }
    }
}
