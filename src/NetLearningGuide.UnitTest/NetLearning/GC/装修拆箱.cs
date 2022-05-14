using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.GC
{
    public class 装箱拆箱
    {
        [Fact]
        public Task 装箱()
        {
            //值类型打包到引用类型
            //需要三个步骤
            //1.托管堆中分配内存
            //2.值类型的字段复制到新分配的堆内存
            //3.返回对象地址,现在该地址是对象引用,值类型成了引用类型
            return Task.CompletedTask;
        }

        [Fact]
        public Task 拆箱()
        {
            //就是从引用数据中提取值类型, int i = obj.i
            return Task.CompletedTask;
        }
    }
}
