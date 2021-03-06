using System;
using Shouldly;
using System.Threading.Tasks;
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
            public string FirstName;//字段 

            public void Save()
            {
                new DataStorage().Store(this);
            }
        }

        class DataStorage
        {
            public void Store(Employment employment)
            {
                employment.FirstName = "Test";
            }
        }
        [Fact]
        public Task ThisTestCase2()
        {
            var employment = new Employment();
            employment.Save();
            employment.FirstName.ShouldBe("Test");
            return Task.CompletedTask;
        }
        #endregion

        #region 属性

        [Fact]
        public Task AtributeTestCase1()
        {
            var person = new Person("Vinson", "T");
            person.FirstName.ShouldBe("Vinson");
            person.LastName.ShouldBe("T");
            person.FullName.ShouldBe("Vinson.T");
            return Task.CompletedTask;
        }

        private class Person
        {
            public Person(string firstName, string lastName)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
            }

            public string FirstName { get; }
            public string LastName { get; }
            public string FullName => $"{FirstName}.{LastName}";
        }

        [Fact]
        public Task AtributeTestCase2()
        {
            var person = new PersonName();
            person.Name = "Vinson T";
            person.LastName.ShouldBe("T");
            person.FirstName.ShouldBe("Vinson");
            person.FirstName = "Fax";
            person.Name.ShouldBe("Fax T");
            return Task.CompletedTask;
        }
        private class PersonName
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Name
            {
                get { return $"{FirstName} {LastName}"; }
                set
                {
                    string[] names;
                    names = value.Split(new[] { ' ' });
                    if (names.Length == 2)
                    {
                        FirstName = names[0];
                        LastName = names[1];
                    }
                    else
                    {
                        throw new ArgumentException($"Assigned vale '{value}' is invalid", "value");
                    }
                }
            }
        }

        public class StaticIdTest
        {
            public StaticIdTest(string ma)
            {
                Ma = ma;
                NextId += 1;
            }
            public string Ma { get; set; }
            public static int NextId = 30;
        }

        [Fact]
        public Task AtributeTestCase3()
        {
            var test = StaticIdTest.NextId;
            test.ShouldBe(30);
            var model = new StaticIdTest("test");
            var test2 = StaticIdTest.NextId;
            test2.ShouldBe(31);
            model.Ma.ShouldBe("test");
            return Task.CompletedTask;
        }
        #endregion

        #region 扩展方法
        [Fact]
        public Task ExtendFunctionCase1()
        {
            var result = new StaticIdTest("").Initail();
            result.Ma.ShouldBe("test");
            return Task.CompletedTask;
        }
        #endregion
        #region 分布类

        public partial class PartialClassTest
        {
            public string A { get; set; }
        }
        public partial class PartialClassTest
        {
            public string B { get; set; }
        }
        [Fact]
        public Task PartialClassTestCase1()
        {
            var result = new PartialClassTest();
            result.A.ShouldBeNull();
            result.B.ShouldBeNull();
            return Task.CompletedTask;
        }

        #endregion

    }
    public static class TestExtend
    {
        public static ChapterFive.StaticIdTest Initail(this ChapterFive.StaticIdTest input)
        {
            return new ChapterFive.StaticIdTest("test");
        }
    }
}
