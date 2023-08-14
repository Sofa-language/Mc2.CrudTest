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

        public Task<bool> IsDuplicatedAsync(string firstName, string lastName, DateTimeOffset dateOfBirth)
        {
            return DbContext.Customers.AnyAsync(c => 
                    c.Firstname.Equals(firstName) && 
                    c.Lastname.Equals(lastName) && 
                    c.DateOfBirth.Equals(dateOfBirth)
                );
        }
    }
}
