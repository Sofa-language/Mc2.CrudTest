using Mc2.CrudTest.Application.Contract.Customers.Dtos;
using Mc2.CrudTest.Application.Contract.Customers.Queries;
using Mc2.CrudTest.Persistence.EntityFramework.Persistence;
using Mc2.CrudTest.Presentation.Shared.Application;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Application.Customers.QueryHandlers
{
    public class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, CustomerDto?>
    {
        private readonly SampleDbContext _dbContext;
        public GetCustomerByIdQueryHandler(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers
                .Where(s => s.Id == request.CustomerId)
                .Select(s=> new CustomerDto(s.Id, s.Firstname, s.Lastname, s.PhoneNumber.Value, s.Email.Value, s.BankAccountNumber.Value, s.DateOfBirth))
                .SingleOrDefaultAsync();

            return customer;
        }
    }
}
