using Mc2.CrudTest.Application.Contract.Customers.Dtos;
using Mc2.CrudTest.Application.Contract.Customers.Queries;
using Mc2.CrudTest.Persistence.EntityFramework.Persistence;
using Mc2.CrudTest.Presentation.Shared.Application;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Application.Customers.QueryHandlers
{
    internal class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, Pagination<CustomerDto>>
    {
        private readonly SampleDbContext _dbContext;
        public GetCustomersQueryHandler(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pagination<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _dbContext.Customers.CountAsync();
            var customers = await _dbContext.Customers
                .Select(s => new CustomerDto(s.Id, s.Firstname, s.Lastname, s.PhoneNumber.Value, s.Email.Value, s.BankAccountNumber.Value, s.DateOfBirth))
                .ToListAsync();

            return new Pagination<CustomerDto>() { Items = customers, TotalItems = totalCount };
        }
    }
}
