using Microsoft.EntityFrameworkCore;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.EFCore;
using NetLearningGuide.Message.Commands.Demo.FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo.FluentValidation
{
    public class FluentValidation : IFluentValidation
    {
        private readonly DbNetContext _dbContext;
        public FluentValidation(DbNetContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> InsertTest(DemoFluentValidationInsertCommand command, CancellationToken cancellationToken)
        {
            await _dbContext.Set<TestDbUp>().AddAsync(new TestDbUp { Guid = command.Id, DescInfo = command.Description }, cancellationToken).ConfigureAwait(false);
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }
        public async Task<bool> UpdateTest(DemoFluentValidationUpdateCommand command, CancellationToken cancellationToken)
        {
            var checkItem = await _dbContext.Set<TestDbUp>().FirstOrDefaultAsync(x => x.Guid == command.Id, cancellationToken).ConfigureAwait(false);
            checkItem.DescInfo = command.Description;
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        } 
    }
}
