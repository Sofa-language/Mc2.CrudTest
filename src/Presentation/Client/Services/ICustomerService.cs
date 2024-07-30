using Mc2.CrudTest.Presentation.Client.Models;

namespace Mc2.CrudTest.Presentation.Client.Services;

public interface ICustomerService
{
    Task<Customer[]> GetCustomerListAsync();
    Task AddCustomerAsync(Customer customer);
    Task<Customer> GetCustomerByIdAsync(long id);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(long id);
}