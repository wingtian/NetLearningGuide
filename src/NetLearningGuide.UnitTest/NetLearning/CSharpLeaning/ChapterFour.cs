using System;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.CSharpLeaning
{
    public class ChapterFour
    {
        #region Ref
        [Fact]
        public Task RefTest()
        {
            string one = "Hello";
            string two = "Bye";
            Swap(ref one, ref two);
            one.ShouldBe("Bye");
            two.ShouldBe("Hello");
            return Task.CompletedTask;
        }

        private void Swap(ref string x, ref string y)
        {
            string temp = x;
            x = y;
            y = temp;
        }
        #endregion

        #region Out  
        //Ref 与Out 的区别， Out 每个正常返回的代码路径都必须对所有out参数进行赋值，而Ref并不需要
        [Fact]
        public Task OutTest()
        {
            int input = 3;
            int output;
            GetNumber(input, out output);
            output.ShouldBe(1);
            return Task.CompletedTask;
        }

        private void GetNumber(int input, out int output)
        {
            output = input % 2;
        }
        #endregion

        #region recursive递归 
        [Fact]
        public Task RecursiveTest()
        {
            var test = Recursive(0);
            test.ShouldBe(10);
            return Task.CompletedTask;
        }

        private int Recursive(int input)
        {
            if (input < 10)
                input = Recursive(input + 1);
            return input;
        }
        #endregion

        #region Override 重载 
        [Fact]
        public Task OverrideTest()
        {
            var test1 = OverrideMethod();
            test1.ShouldBe(1);
            var test2 = OverrideMethod(2);
            test2.ShouldBe(2);
            return Task.CompletedTask;
        }

        private int OverrideMethod()
        {
            return 1;
        }

        private int OverrideMethod(int input)
        {
            return input;
        }
        #endregion

        #region Default Param 
        [Fact]
        public Task DefaultParamTest()
        {
            var test = DisplayGreeting(firstName: "Test", lastName: "BB");
            test.ShouldBe("TestBB");
            return Task.CompletedTask;
        }

        private string DisplayGreeting(string firstName, string middleName = default, string lastName = default)
        {
            return firstName + middleName + lastName;
        }

        #endregion

        #region MethodResolution
        [Fact]
        public Task MethodResolutionTest()
        {
            var test = Method(40);
            test.ShouldBe(40); //long 比 double和object更具体所以选long
            test = Method(41d);
            test.ShouldBe(41);
            test = Method("42");
            test.ShouldBe(42);
            return Task.CompletedTask;
        }

        private int Method(object thing)
        {
            return Convert.ToInt16(thing);
        }
        private int Method(double thing)
        {
            return Convert.ToInt16(thing);
        }
        private int Method(long thing)
        {
            return Convert.ToInt16(thing);
        }

        #endregion

        #region tryparse
        [Fact]
        public Task TryParseTest()
        {
            int.TryParse("1", out var output).ShouldBeTrue();
            output.ShouldBe(1);
            int.TryParse("1.12", out output).ShouldBeFalse();
            output.ShouldBe(0);
            return Task.CompletedTask;
        }

        #endregion
    }
}
