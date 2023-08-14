namespace Mc2.CrudTest.Domain.Customers.DomainServices
{
    public interface ICustomerDuplicationValidatorService
    {
        Task<bool> IsValidAsync(string firstName, string lastName, DateTimeOffset dateOfBirth);
    }
    internal class CustomerDuplicationValidatorService : ICustomerDuplicationValidatorService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerDuplicationValidatorService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<bool> IsValidAsync(string firstName, string lastName, DateTimeOffset dateOfBirth)
        {
            return _customerRepository.IsDuplicatedAsync(firstName, lastName, dateOfBirth);
        }
    }
}
