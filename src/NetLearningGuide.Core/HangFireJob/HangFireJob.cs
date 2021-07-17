using Hangfire;
using NetLearningGuide.Core.Services.Demo.EfCore;
using NetLearningGuide.Message.Commands.Demo;
using System;

namespace NetLearningGuide.Core.HangFireJob
{
    public static class HangFireJob
    {
        public static void ExecuteRecurringJob()
        {
            //添加一個每小時執行一次的週期性任務,並立即執行一次
            RecurringJob.AddOrUpdate<IEfCoreTestService>("InsertRandom", x => x.InsertTest(new DemoEfInsertCommand(Guid.NewGuid()) { Description = "InsertRandom" }, default), "0 0 0/1 * * ? ");
            RecurringJob.Trigger("InsertRandom");
        }
    }
}
