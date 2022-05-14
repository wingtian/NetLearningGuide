using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
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
            //第0代,临时变量, 第0代满的时候gc才会
            //第1代,缓冲期,短生存对象
            //第2代,长生存期对象,静态参数以及大对象
            //大对象:大于85000字节

            object o = new byte[85000];
            System.GC.GetGeneration(o).ShouldBe(2);

            object min = new byte[850];
            System.GC.GetGeneration(min).ShouldBe(0);

            object o1 = new object();
            System.GC.GetGeneration(o1).ShouldBe(0);

            System.GC.Collect();
            System.GC.GetGeneration(o1).ShouldBe(1);

            System.GC.Collect();
            System.GC.GetGeneration(o1).ShouldBe(2);

            o1 = null;
            System.GC.Collect(01, GCCollectionMode.Forced);
            System.GC.GetGeneration(o1).ShouldBe(1);
            return Task.CompletedTask;
        }

    }
}
