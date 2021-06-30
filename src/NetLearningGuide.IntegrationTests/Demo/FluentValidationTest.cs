using Microsoft.EntityFrameworkCore;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.EFCore;
using NetLearningGuide.Core.Validators;
using Shouldly;
using System;
using System.Threading.Tasks;
using Mediator.Net;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo.FluentValidation;
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
                    await dbContext.Set<TestDbUp>().AddAsync(test).ConfigureAwait(false);
                    await dbContext.SaveChangesAsync().ConfigureAwait(false);
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
            var checkItem = new TestDbUp();
            await Run<IEntityFluentValidator, DbNetContext>(async (validator, dbContext) =>
            {
                try
                {
                    await dbContext.Set<TestDbUp>().AddAsync(test).ConfigureAwait(false);
                    await dbContext.SaveChangesAsync();
                    checkItem = await dbContext.Set<TestDbUp>().FirstOrDefaultAsync(x => x.Guid == test.Guid).ConfigureAwait(false);
                    checkItem.DescInfo = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    dbContext.Set<TestDbUp>().Remove(checkItem);
                    await dbContext.SaveChangesAsync().ConfigureAwait(false);
                    e.Message.ShouldContain("描述长度在1到45之间!");
                }
            });
        }

        [Fact]
        public async Task ValidationInsertTestCase1()
        {
            var test = new DemoFluentValidationInsertCommand(Guid.NewGuid())
            {
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            try
            {
                await Run<IMediator>(async (mediator) =>
                {
                    await mediator.SendAsync<DemoFluentValidationInsertCommand, CommonResponse<bool>>(test);
                });
            }
            catch (Exception e)
            {
                e.Message.ShouldContain("描述长度在1到45之间!");
            }
        }

        [Fact]
        public async Task ValidationUpdateTestCase1()
        {
            var test = new TestDbUp()
            {
                Guid = Guid.NewGuid(),
                DescInfo = "a"
            };
            var command = new DemoFluentValidationUpdateCommand(test.Guid)
            {
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            try
            {
                await Run<IEntityFluentValidator, DbNetContext, IMediator>(async (validator, dbContext, mediator) =>
                {
                    try
                    {
                        await dbContext.Set<TestDbUp>().AddAsync(test).ConfigureAwait(false);
                        await dbContext.SaveChangesAsync();
                        await mediator.SendAsync<DemoFluentValidationUpdateCommand, CommonResponse<bool>>(command);
                        await dbContext.SaveChangesAsync().ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        dbContext.Set<TestDbUp>().Remove(test);
                        await dbContext.SaveChangesAsync().ConfigureAwait(false);
                        e.Message.ShouldContain("描述长度在1到45之间!");
                    }
                });
            }
            catch (Exception e)
            {
                e.Message.ShouldContain("描述长度在1到45之间!");
            }
        }
    }
}
