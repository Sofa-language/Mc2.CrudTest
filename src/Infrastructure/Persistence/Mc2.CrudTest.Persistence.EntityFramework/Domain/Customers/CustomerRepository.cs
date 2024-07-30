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
            return DbContext.Customers.SingleOrDefaultAsync(c => c.Email.Value.Equals(email));
        }

        public async Task<bool> IsDuplicatedAsync(long? id, string firstName, string lastName, DateTimeOffset dateOfBirth)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(c =>
                    c.Firstname.Equals(firstName) &&
                    c.Lastname.Equals(lastName) &&
                    c.DateOfBirth.Equals(dateOfBirth)
                );

            return customer == null || customer.Id == id;
        }
    }
}
