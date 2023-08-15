using Mc2.CrudTest.Domain.Customers.DomainServices;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Domain.Customers.Initializers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;
using System.Runtime.CompilerServices;

namespace Mc2.CrudTest.Domain.Customers
{
    public class Customer : Entity<long>, IAggregateRoot
    {
        private Customer() : base()
        {
            
        }
        private Customer(CreateOrUpdateInitializer initializer) : this()
        {
            Id = initializer.Id;
            Firstname = initializer.Firstname;
            Lastname = initializer.Lastname;
            DateOfBirth = initializer.DateOfBirth;
            PhoneNumber = initializer.PhoneNumber;
            BankAccountNumber = initializer.BankAccountNumber;
        }
        public long Id { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public DateTimeOffset DateOfBirth { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Email Email { get; private set; }
        public BankAccountNumber BankAccountNumber { get; private set; }

        #region PrivateMethods
        private async Task SetEmail(string email, IEmailAddressDuplicationValidatorService emailAddressDuplicationService)
        {
            var isUnique = await emailAddressDuplicationService.IsValidAsync(Id, email);
            if (!isUnique)
                throw new DuplicatedEmailException(ExceptionsEnum.DuplicatedEmailException, email);

            Email = email;
        }
        #endregion

        public static async Task<Customer> CreateAsync(CreateOrUpdateInitializer initializer, 
            IEmailAddressDuplicationValidatorService emailAddressDuplicationService,
            ICustomerDuplicationValidatorService customerDuplicationValidatorService)
        {
            var isCustomerUnique = await customerDuplicationValidatorService.IsValidAsync(null, initializer.Firstname, initializer.Lastname, initializer.DateOfBirth);
            if (!isCustomerUnique)
                throw new CustomerDuplicatedException(ExceptionsEnum.CustomerDuplicatedException);

            var customer = new Customer(initializer);

            await customer.SetEmail(initializer.Email, emailAddressDuplicationService);

            return customer;
        }

        public async Task UpdateAsync(string expectedFirstname, string expectedLastname, string expectedEmail, 
            string expectedPhoneNumber, string expectedBankAccountNumber, DateTimeOffset expectedDateOfBirth, 
            IEmailAddressDuplicationValidatorService emailAddressDuplicationService, 
            ICustomerDuplicationValidatorService customerDuplicationValidatorService)
        {
            var isCustomerUnique = await customerDuplicationValidatorService.IsValidAsync(this.Id, expectedFirstname, expectedLastname, expectedDateOfBirth);
            if (!isCustomerUnique)
                throw new CustomerDuplicatedException(ExceptionsEnum.CustomerDuplicatedException);

            await this.SetEmail(expectedEmail, emailAddressDuplicationService);

            Firstname = expectedFirstname;
            Lastname = expectedLastname;
            DateOfBirth = expectedDateOfBirth;
            PhoneNumber = expectedPhoneNumber;
            BankAccountNumber = expectedBankAccountNumber;
        }
    }
}
