using Mc2.CrudTest.Domain.Contract.Customers.Events;
using Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Customers.DomainEventConsumers
{
    public class CreateCustomerDomainEventHandler : DomainEventHandler<CreateCustomerDomainEvent>
    {
        public CreateCustomerDomainEventHandler(ILogger logger) : base(logger)
        {
        }

        protected override async Task HandleEvent(CreateCustomerDomainEvent notification, CancellationToken cancellationToken)
        {
            //Publish on a MessageBrocker on write on a secondary Database
            await Task.FromResult(true);
        }
    }
}
