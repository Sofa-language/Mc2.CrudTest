using Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent;

namespace Mc2.CrudTest.Domain.Contract.Customers.Events
{
    public class DeleteCustomerDomainEvent : DomainEventBase
    {
        public long Id { get; set; }

        public DeleteCustomerDomainEvent(long id)
        {
            Id = id;
        }
    }
}
