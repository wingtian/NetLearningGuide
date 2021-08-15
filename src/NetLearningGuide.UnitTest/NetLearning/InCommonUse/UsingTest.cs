using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class UsingTest
    {
        [Fact]
        public Task UsingTestCase1()
        {
            UsingTestModel test;
            using (var us = new UsingTestClass())
            {
                test = us.Model ;
            }
            test.Output.ShouldBe("TEST");
            return Task.CompletedTask;
        }
    }
    public class UsingTestClass : IDisposable
    {
        public UsingTestModel Model;

        public UsingTestClass()
        {
            Model = GetModel();
        }

        private UsingTestModel GetModel()
        {
            return new UsingTestModel("TEST");
        }

        public void Dispose()
        {
            Model = null;
        }
    }

    public class UsingTestModel
    {
        public UsingTestModel(string output)
        {
            Output = output;
        }
        public string Output { get; set; }
    }
}
