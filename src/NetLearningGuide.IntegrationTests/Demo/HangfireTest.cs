using System.Threading.Tasks;
using Autofac;
using Hangfire;
using Microsoft.Extensions.Caching.Memory;
using NetLearningGuide.Core.HangFireJob;
using Xunit;

namespace NetLearningGuide.IntegrationTests.Demo
{
    public class HangfireTest : TestBase
    {
        [Fact]
        public async Task HangfireJobTestCase1()
        {
            //using (new BackgroundJobServer())
            //{
            //    HangFireJob.ExecuteRecurringJob();
            //}
        }
    }
}
