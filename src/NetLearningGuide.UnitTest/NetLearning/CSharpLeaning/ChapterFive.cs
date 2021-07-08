using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.CSharpLeaning
{
    public class ChapterFive
    {
        #region This Case1  不建议这样用，不符合命名规范。 
        class Employee
        {
            public string _firstName;//字段

            public void SetName(string _firstName)//参数 or 局部变量
            {
                this._firstName = _firstName;
            }
        }
        [Fact]
        public Task ThisTestCase1()
        {
            var employee = new Employee();
            employee.SetName("Test");
            employee._firstName.ShouldBe("Test");
            return Task.CompletedTask;
        }
        #endregion

        #region This 正确用法
        class Employment
        {
            public string _firstName;//字段 

            public void Save()
            {
                new DataStorage().Store(this);
            }
        }

        class DataStorage
        {
            public void Store(Employment employment)
            {
                employment._firstName = "Test";
            }
        }
        [Fact]
        public Task ThisTestCase2()
        {
            var employment = new Employment();
            employment.Save();
            employment._firstName.ShouldBe("Test");
            return Task.CompletedTask;
        }
        #endregion
    }
}
