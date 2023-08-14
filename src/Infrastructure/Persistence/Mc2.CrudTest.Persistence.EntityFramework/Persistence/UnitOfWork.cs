using Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Persistence.EntityFramework.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleDbContext _dbContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        public UnitOfWork(SampleDbContext dbContext,
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            _dbContext = dbContext;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(CancellationToken.None))
            {
                try
                {
                    await _domainEventsDispatcher.DispatchEventsAsync();
                    var result = await _dbContext.SaveChangesAsync(cancellationToken);

                    //ignore cancellation token
                    await transaction.CommitAsync(CancellationToken.None);
                    return result;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(CancellationToken.None);
                    throw;
                }
            }
        }
    }
}
