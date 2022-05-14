using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.GC
{
    public class 垃圾回收
    {
        [Fact]
        private Task 垃圾回收GarbageCollection()
        {
            //自动回收堆上的内存
            return Task.CompletedTask;
        }
        [Fact]
        private Task 垃圾回收的代()
        {
            //对象越新,生存期越短
            //对象越老,生存期越长
            //回收堆的一部分,速度快于回收整个堆
            //托管堆分为3代,第0,1,2代,可以单独处理长生存期和短生存期对象
            return Task.CompletedTask;
        }
    }
}
