using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Persistence.EntityFramework.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Persistence.EntityFramework.Domain.Customers
{
    public class CustomerRepository : RepositoryBase<Customer, long>, ICustomerRepository
    {
        public CustomerRepository(SampleDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Customer> GetByEmailAsync(string email)
        {
            return DbContext.Customers.SingleOrDefaultAsync(c=> c.Email.Value.Equals(email));
        }
    }
}
