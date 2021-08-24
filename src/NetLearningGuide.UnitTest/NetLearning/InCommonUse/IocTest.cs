using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class IocTest
    {
        [Fact]
        public Task IocTestCase1()
        {
            var case1 = new IocCase1();
            var program1 = new IocProgram(case1);
            program1.Inter.Hello().ShouldBe("Hello IocCase1");

            var case2 = new IocCase2();
            var program2 = new IocProgram(case2);
            program2.Inter.Hello().ShouldBe("Hello IocCase2");
            return Task.CompletedTask;
        }
    }

    public interface IOcInterface
    {
        string Hello();
    }

    public class IocCase1 : IOcInterface
    {
        public string Hello()
        {
            return "Hello IocCase1";
        }
    }
    public class IocCase2 : IOcInterface
    {
        public string Hello()
        {
            return "Hello IocCase2";
        }
    }

    public class IocProgram
    {
        public readonly IOcInterface Inter;

        public IocProgram(IOcInterface inter)
        {
            this.Inter = inter;
        }
    }

}
