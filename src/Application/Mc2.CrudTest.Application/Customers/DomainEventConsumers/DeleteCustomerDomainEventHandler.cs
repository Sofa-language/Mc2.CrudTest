using Mc2.CrudTest.Domain.Contract.Customers.Events;
using Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Customers.DomainEventConsumers
{
    public class DeleteCustomerDomainEventHandler : DomainEventHandler<DeleteCustomerDomainEvent>
    {
        public DeleteCustomerDomainEventHandler(ILogger logger) : base(logger)
        {
        }

        protected override async Task HandleEvent(DeleteCustomerDomainEvent notification, CancellationToken cancellationToken)
        {
            //Publish on a MessageBrocker on write on a secondary Database
            await Task.FromResult(true);
        }
    }
}
