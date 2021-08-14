using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class NewClassTest
    {
        public class NewTemp
        {
            public NewTemp(int status)
            {
                Status = status;
            }

            public int Status { get; set; }
        }

        private NewTemp GetTemp(NewTemp temp)
        {
            return temp;
        }
        [Fact]
        public Task NewClassTestCase1()
        {
            NewTemp temp = GetTemp(null);
            try
            {
                if (temp.Status == 1) //Reshaper 没提示
                {
                }
            }
            catch (Exception e)
            {
                e.Message.ShouldBe("Object reference not set to an instance of an object.");
            }
            return Task.CompletedTask;
        }
    }
}
