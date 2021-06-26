using Microsoft.EntityFrameworkCore;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.EFCore;
using NetLearningGuide.Core.Validators;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.IntegrationTests.Demo
{
    public class FluentValidationTest : TestBase
    {
        [Fact]
        public async Task ValidationInsertTest()
        {
            try
            {
                var test = new TestDbUp()
                {
                    Guid = Guid.NewGuid(),
                    DescInfo = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                };
                await Run<IEntityFluentValidator, DbNetContext>(async (validator, dbContext) =>
                {
                    await dbContext.Set<TestDbUp>().AddAsync(test, default).ConfigureAwait(false);
                    await dbContext.SaveChangesAsync(default);
                });
            }
            catch (Exception e)
            {
                e.Message.ShouldContain("描述长度在1到45之间!");
            }
        }
        [Fact]
        public async Task ValidationUpdateTest()
        {

            var test = new TestDbUp()
            {
                Guid = Guid.NewGuid(),
                DescInfo = "a"
            };
            var checkItem = new TestDbUp(); ;
            await Run<IEntityFluentValidator, DbNetContext>(async (validator, dbContext) =>
            {
                try
                {
                    await dbContext.Set<TestDbUp>().AddAsync(test, default).ConfigureAwait(false);
                    await dbContext.SaveChangesAsync(default);
                    checkItem = await dbContext.Set<TestDbUp>().FirstOrDefaultAsync(x => x.Guid == test.Guid, default).ConfigureAwait(false);
                    checkItem.DescInfo = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    await dbContext.SaveChangesAsync(default);
                }
                catch (Exception e)
                {
                    dbContext.Set<TestDbUp>().Remove(checkItem);
                    await dbContext.SaveChangesAsync(default);
                    e.Message.ShouldContain("描述长度在1到45之间!");
                }
            });

        }
    }
}
