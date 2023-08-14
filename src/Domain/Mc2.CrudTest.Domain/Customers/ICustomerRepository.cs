using Mc2.CrudTest.Presentation.Shared.Shared;

namespace Mc2.CrudTest.Domain.Customers
{
    public interface ICustomerRepository : IRepository<Customer, long>
    {
        Task<Customer> GetByEmailAsync(string email);
        Task<bool> IsDuplicatedAsync(string firstName, string lastName, DateTimeOffset dateOfBirth);
    }
}
