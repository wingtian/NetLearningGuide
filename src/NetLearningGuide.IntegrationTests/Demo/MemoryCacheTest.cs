using Microsoft.Extensions.Caching.Memory;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.EFCore;
using NetLearningGuide.Core.Services.Demo.EfCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.IntegrationTests.Demo
{
    public class MemoryCacheTest : TestBase
    {
        [Fact]
        public async Task TestMemory()
        {
            await Run<DbNetContext, IMemoryCache>(async (context, memoryCache) =>
            {
                var service = new EfCoreTestService(context, memoryCache);
                var initList = new List<TestDbUp>();
                for (int i = 0; i < 30000; i++)
                {
                    initList.Add(new TestDbUp() { Guid = Guid.NewGuid(), DescInfo = "123" });
                }
                await context.Set<TestDbUp>().AddRangeAsync(initList).ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);

                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();  //开始监视代码运行时间  
                await service.GetAllTest(default).ConfigureAwait(false);
                watch.Stop();  //停止监视
                var timespan1 = watch.Elapsed;  //获取当前实例测量得出的总时间

                watch.Reset();
                watch.Start();  //开始监视代码运行时间  
                await service.GetAllTest(default).ConfigureAwait(false);
                watch.Stop();  //停止监视
                var timespan2 = watch.Elapsed;  //获取当前实例测量得出的总时间

                watch.Reset();
                watch.Start();  //开始监视代码运行时间  
                await service.GetAllTestNoCache(default).ConfigureAwait(false);
                watch.Stop();  //停止监视
                var timespan3 = watch.Elapsed;  //获取当前实例测量得出的总时间

                context.RemoveRange(initList);
                await context.SaveChangesAsync().ConfigureAwait(false);
                timespan1.ShouldBeGreaterThan(timespan2);
                timespan3.ShouldBeGreaterThan(timespan2);
            });
        }
    }
}
