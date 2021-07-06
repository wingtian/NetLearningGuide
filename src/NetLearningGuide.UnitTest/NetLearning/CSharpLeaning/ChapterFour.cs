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

        #endregion
    }
}
