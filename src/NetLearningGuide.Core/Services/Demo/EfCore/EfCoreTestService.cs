using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.EFCore;
using NetLearningGuide.Message.Commands.Demo;

namespace NetLearningGuide.Core.Services.Demo.EfCore
{
    public class EfCoreTestService : IEfCoreTestService
    {
        private readonly DbNetContext _dbContext;
        public EfCoreTestService(DbNetContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> InsertTest(DemoEfInsertCommand model, CancellationToken cancellationToken)
        {
            await _dbContext.Set<TestDbUp>().AddAsync(new TestDbUp { Guid = model.Id, DescInfo = model.Description }, cancellationToken).ConfigureAwait(false);
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }
        public async Task<bool> UpdateTest(Guid guid, CancellationToken cancellationToken)
        {
            var checkItem = await _dbContext.Set<TestDbUp>().FirstOrDefaultAsync(x => x.Guid == guid, cancellationToken).ConfigureAwait(false);
            checkItem.DescInfo = "更新測試";
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }
        public async Task<TestDbUp> GetTest(Guid guid, CancellationToken cancellationToken)
        {
            var checkItem = await _dbContext.Set<TestDbUp>().FirstOrDefaultAsync(x => x.Guid == guid, cancellationToken).ConfigureAwait(false);
            return checkItem;
        }
        public async Task<bool> DeleteTest(Guid guid, CancellationToken cancellationToken)
        {
            var checkItem = await _dbContext.Set<TestDbUp>().FirstOrDefaultAsync(x => x.Guid == guid, cancellationToken).ConfigureAwait(false);
            if (checkItem == null)
                return false;
            _dbContext.Set<TestDbUp>().Remove(checkItem);
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }
    }
}
