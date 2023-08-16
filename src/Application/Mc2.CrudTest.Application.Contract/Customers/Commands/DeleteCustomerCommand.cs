using Mc2.CrudTest.Presentation.Shared.Application;

namespace Mc2.CrudTest.Application.Contract.Customers.Commands
{
    public class DeleteCustomerCommand : CommandBase
    {
        public long CustomerId { get; set; }
    }
}
