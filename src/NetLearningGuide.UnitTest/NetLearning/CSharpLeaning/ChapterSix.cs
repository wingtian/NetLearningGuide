using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.CSharpLeaning
{
    public class ChapterSix
    {
        #region 隐式转换
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task ImplicitTestCase1()
        {
            UtmCoordinates test = new GpsCoordinates();
            test.Test.ShouldBe("HHH");
            return Task.CompletedTask;
        }

        class GpsCoordinates
        {
            public static implicit operator UtmCoordinates(GpsCoordinates gps)
            {
                return new UtmCoordinates()
                {
                    Test = "HHH"
                };
            }
        }

        class UtmCoordinates
        {
            public string Test { get; set; }
        }
        #endregion
        #region 显式转换
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task ExplicitTestCase1()
        {
            UtmCoordinatesCase2 test = (UtmCoordinatesCase2)(new GpsCoordinatesCase2()); //(UtmCoordinatesCase2) 代表显示转换
            test.Test.ShouldBe("HHH");
            return Task.CompletedTask;
        }

        class GpsCoordinatesCase2
        {
            public static explicit operator UtmCoordinatesCase2(GpsCoordinatesCase2 gps)
            {
                return new UtmCoordinatesCase2()
                {
                    Test = "HHH"
                };
            }
        }
        class UtmCoordinatesCase2
        {
            public string Test { get; set; }
        }
        #endregion 
        #region protected 访问修饰符 
        public class ProtectTest
        {
            protected Guid Id { get; set; }
        }

        public class ContactTest : ProtectTest
        {
            void Save()
            {
                var test = Id;
            }

            void Load(ProtectTest test)
            {
                // 错误  CS1540 无法通过“ChapterSix.ProtectTest”类型的限定符访问受保护的成员“ChapterSix.ProtectTest.Id”；
                // 限定符必须是“ChapterSix.ContactTest”类型(或者从该类型派生)
                //var model = test.Id; 
            }
        }
        #endregion 
        #region 重写属性

        public class PdaItem
        {
            public virtual string Name { get; set; }
        }

        public class Contact : PdaItem
        {
            public override string Name
            {
                get { return $"{FirstName} {LastName}"; }
                set
                {
                    string[] names = value.Split(' ');
                    FirstName = names[0];
                    LastName = names[1];
                }
            }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        [Fact]
        public Task OverrideTestCase()
        {
            //"运行时"调用虚方法派生得最远的实现
            Contact contact = new Contact();
            PdaItem item = contact;
            item.Name = "VINSON TIAN";
            contact.FirstName.ShouldBe("VINSON");
            contact.LastName.ShouldBe("TIAN");
            return Task.CompletedTask;
        }
        #endregion

        #region Override new 

        public class Program
        {
            public class BaseClass
            {
                public string DisplayName()
                {
                    return "BaseClass";
                }
            }

            public class DerivedClass : BaseClass
            {
                public virtual string DisplayName()
                {
                    return "DerivedClass";
                }
            }
            public class SubDerivedClass : DerivedClass
            {
                public override string DisplayName()
                {
                    return "SubDerivedClass";
                }
            }
            public class SuperSubDerivedClass : SubDerivedClass
            {
                public new string DisplayName()
                {
                    return "SuperSubDerivedClass";
                }
            }
        }
        [Fact]
        public Task OverrideNewTestCase()
        {
            var superSubDerivedClass = new Program.SuperSubDerivedClass();
            Program.SubDerivedClass subDerivedClass = superSubDerivedClass;
            Program.DerivedClass derivedClass = superSubDerivedClass;
            Program.BaseClass baseClass = superSubDerivedClass;
            var test1 = superSubDerivedClass.DisplayName();
            var test2 = subDerivedClass.DisplayName();
            var test3 = derivedClass.DisplayName();
            var test4 = baseClass.DisplayName();
            test1.ShouldBe("SuperSubDerivedClass");
            test2.ShouldBe("SubDerivedClass");
            test3.ShouldBe("SubDerivedClass");
            test4.ShouldBe("BaseClass");
            return Task.CompletedTask;
        }
        #endregion

        #region 抽象类

        public abstract class PdaItems
        {
            public PdaItems(string name)
            {
                Name = name;
            }
            public virtual string Name { get; set; }
            public abstract string GetSummary();
        }

        public class Contacts : PdaItems
        {
            public Contacts(string name) : base(name)
            {
                Name = name + "abc";
            }
            public override string GetSummary()
            {
                return "ABC";
            }
            public sealed override string Name { get; set; }
        }

        [Fact]
        public Task AbstractTestCase()
        { 
            Contacts contacts = new Contacts("Test");
            PdaItems items = contacts;
            contacts.Name.ShouldBe("Testabc");
            items.Name.ShouldBe("Testabc");
            return Task.CompletedTask;
        }

        #endregion
    }
}
