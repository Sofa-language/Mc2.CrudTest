using Mc2.CrudTest.Persistence.EntityFramework.Persistence;
using Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent;
using Mc2.CrudTest.Presentation.Shared.SeedWork;
using MediatR;

namespace Mc2.CrudTest.Persistence.EntityFramework.EventProcessing
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly SampleDbContext _dbContext;

        public DomainEventsDispatcher(IMediator mediator, SampleDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = this._dbContext.ChangeTracker
                .Entries<Entity<long>>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var tasks = domainEvents.Select(e => _mediator.Publish(e));

            await Task.WhenAll(tasks);
        }
    }
}
