using Mc2.CrudTest.Domain.Contract.Customers.Events;
using Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Customers.DomainEventConsumers
{
    public class UpdateCustomerDomainEventHandler : DomainEventHandler<UpdateCustomerDomainEvent>
    {
        public UpdateCustomerDomainEventHandler(ILogger logger) : base(logger)
        {
        }

        protected override async Task HandleEvent(UpdateCustomerDomainEvent notification, CancellationToken cancellationToken)
        {
            //Publish on a MessageBrocker on write on a secondary Database
            await Task.FromResult(true);
        }
    }
}
