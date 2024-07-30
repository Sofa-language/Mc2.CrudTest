using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers.DomainServices
{
    public interface IEmailAddressDuplicationValidatorService
    {
        Task<bool> IsValidAsync(long? id, string email);
    }
    public class EmailAddressDuplicationValidatorService : IEmailAddressDuplicationValidatorService
    {
        private readonly ICustomerRepository _customerRepository;

        public EmailAddressDuplicationValidatorService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> IsValidAsync(long? id, string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);

            return customer == null || (id != null && customer.Id == id);
        }
    }
}
