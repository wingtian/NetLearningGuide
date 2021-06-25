using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.EFCore;
using NetLearningGuide.Core.Validators;
using NetLearningGuide.Core.Validators.Demo;
using Xunit;

namespace NetLearningGuide.IntegrationTests.Demo
{
    public class FluentValidationTest : TestBase
    {
        [Fact]
        public async Task ValidationTest()
        {
           // var test = new TestDbUp()
           // {
           //     Guid = Guid.NewGuid(),
           //     DescInfo = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
           // };

           // await Run<IEntityFluentValidator, DbNetContext>(async (validator, dbContext) =>
           //{
           //    await dbContext.Set<TestDbUp>().AddAsync(test, default).ConfigureAwait(false);
           //    if (validator.EntityType == typeof(TestDbUp))
           //    {
           //        //dbContext.Entry(test).State = EntityState.Added;
           //        var result = validator.Validate();
           //    }
           //});
            //test.IsValid 
        }
    }
}
