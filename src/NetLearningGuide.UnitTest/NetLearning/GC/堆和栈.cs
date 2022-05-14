using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.GC
{
    public class 堆和栈
    {

        [Fact]
        public Task 栈Heap()
        {
            //编译期间就分配好的内存空间,因此代码中必须对栈的大小有明确定义
            //栈的存储是基本值类型
            //像队列先进先出,像是排列好的箱子
            return Task.CompletedTask;
        }
        [Fact]
        public Task 堆Stack()
        {
            // 在程序运行期间动态分配的内存空间,你可以根据程序的运行情况确定要分配的堆内存的大小.
            // 堆存储是new出来的对象
            // 引用类型在栈用存储一个引用,实际存储位置位于托管堆
            // 像仓库
            return Task.CompletedTask;
        }
    }
}
