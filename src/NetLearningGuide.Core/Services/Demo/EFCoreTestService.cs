using Microsoft.EntityFrameworkCore;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.EFCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo
{
    public class EFCoreTestService : IEFCoreTestService
    {
        private readonly DbNetContext _dbContext;
        public EFCoreTestService(DbNetContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> InsertTest(Guid guid, CancellationToken cancellationToken)
        {
            await _dbContext.Set<TestDbUp>().AddAsync(new TestDbUp { GUID = guid, DescInfo = "添加測試" }, cancellationToken).ConfigureAwait(false);
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }
        public async Task<bool> UpdateTest(Guid guid, CancellationToken cancellationToken)
        {
            var checkItem = await _dbContext.Set<TestDbUp>().SingleAsync(x => x.GUID == guid, cancellationToken).ConfigureAwait(false);
            checkItem.DescInfo = "更新測試";
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }
        public async Task<TestDbUp> GetTest(Guid guid, CancellationToken cancellationToken)
        {
            var checkItem = await _dbContext.Set<TestDbUp>().SingleAsync(x => x.GUID == guid, cancellationToken).ConfigureAwait(false);
            return checkItem;
        }
    }
}
