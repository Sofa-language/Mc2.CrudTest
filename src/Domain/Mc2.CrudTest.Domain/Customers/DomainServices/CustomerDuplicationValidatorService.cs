namespace Mc2.CrudTest.Domain.Customers.DomainServices
{
    public interface ICustomerDuplicationValidatorService
    {
        Task<bool> IsValidAsync(long? id, string firstName, string lastName, DateTimeOffset dateOfBirth);
    }
    public class CustomerDuplicationValidatorService : ICustomerDuplicationValidatorService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerDuplicationValidatorService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<bool> IsValidAsync(long? id, string firstName, string lastName, DateTimeOffset dateOfBirth)
        {
            return _customerRepository.IsDuplicatedAsync(id, firstName, lastName, dateOfBirth);
        }
    }
}
