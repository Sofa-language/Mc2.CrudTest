using Mc2.CrudTest.Application.Contract.Customers.Dtos;
using Mc2.CrudTest.Presentation.Shared.Application;

namespace Mc2.CrudTest.Application.Contract.Customers.Queries
{
    public class GetCustomerByIdQuery : IQuery<CustomerDto?>
    {
        public GetCustomerByIdQuery(long customerId)
        {
            CustomerId = customerId;
        }
        public long CustomerId { get; init; }
    }
}
