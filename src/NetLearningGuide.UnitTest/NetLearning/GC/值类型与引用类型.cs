using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.GC
{
    public class 值类型与引用类型
    {
        [Fact]
        public Task 值类型()
        {
            // sbyte , short , int ,long
            // byte , ushort ,uint, ulong
            // char
            // float, double
            // decimal
            // bool
            // enum
            // struct
            int age1 = 10;
            int age2 = age1;
            age2 = 20;  //存在栈

            return Task.CompletedTask;
        }

        [Fact]
        public Task 引用类型()
        {
            // object string class
            // interface
            // int[] , int[,]
            // delegate
            Student stu;
            stu = new Student() { Age = 18 };

            Student stu2 = stu; //地址存在栈
            stu2.Age = 20;//对象值存在堆
            return Task.CompletedTask;
        }

        [Fact]
        public Task 参数值传递Case1()
        {
            Student stu;
            stu = new Student() { Age = 18 };
            ChangeAge(stu.Age);//修改栈的
            stu.Age.ShouldBe(18);
            return Task.CompletedTask;
        }

        void ChangeAge(int age)
        {
            age += 10;
        }


        private class Student
        {
            public int Age { get; set; }
        }
        [Fact]
        public Task 参数值传递Case2()
        {
            Student stu;
            stu = new Student() { Age = 18 };
            ChangeCase2(stu);
            stu.Age.ShouldBe(18);
            return Task.CompletedTask;
        }

        void ChangeCase2(Student stu)
        {
            stu = new Student();//堆上重新分配一个地址, 对应的堆也是新的,不会影响外面的对象
            stu.Age = 19;
        }

        [Fact]
        public Task 参数值传递Case3()
        {
            Student stu;
            stu = new Student() { Age = 18 };
            ChangeCase3(stu);
            stu.Age.ShouldBe(19);
            return Task.CompletedTask;
        }

        void ChangeCase3(Student stu)
        {
            stu.Age = 19;
        } 

        [Fact]
        public Task 参数值传递Case4()
        {
            int age = 10;
            ChangeAgeCase4(ref age);
            age.ShouldBe(20);
            return Task.CompletedTask;
        }

        void ChangeAgeCase4(ref int age)
        {
            age += 10;
        }
    }
}
