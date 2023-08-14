using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Persistence.EntityFramework.Persistence;

namespace Mc2.CrudTest.Persistence.EntityFramework.Domain.Customers
{
    public class CustomerRepository : RepositoryBase<Customer, long>, ICustomerRepository
    {
        public CustomerRepository(SampleDbContext dbContext) : base(dbContext)
        {
        }

    }
}
