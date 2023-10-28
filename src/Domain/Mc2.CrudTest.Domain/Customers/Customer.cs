using Mc2.CrudTest.Domain.Contract.Customers.Events;
using Mc2.CrudTest.Domain.Customers.DomainServices;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Domain.Customers.Initializers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

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
        public FirstName Firstname { get; private set; }
        public LastName Lastname { get; private set; }
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

            customer.AddDomainEvent(new CreateCustomerDomainEvent(customer.Id, customer.Firstname.Value, customer.Lastname.Value,
                customer.Email.Value, customer.PhoneNumber.Value, customer.BankAccountNumber.Value, customer.DateOfBirth));

            return customer;
        }

        public async Task UpdateAsync(CreateOrUpdateInitializer initializer,
            IEmailAddressDuplicationValidatorService emailAddressDuplicationService,
            ICustomerDuplicationValidatorService customerDuplicationValidatorService)
        {
            var isCustomerUnique = await customerDuplicationValidatorService.IsValidAsync(this.Id, initializer.Firstname,
                initializer.Lastname, initializer.DateOfBirth);
            if (!isCustomerUnique)
                throw new CustomerDuplicatedException(ExceptionsEnum.CustomerDuplicatedException);

            await this.SetEmail(initializer.Email, emailAddressDuplicationService);

            Firstname = initializer.Firstname;
            Lastname = initializer.Lastname;
            DateOfBirth = initializer.DateOfBirth;
            PhoneNumber = initializer.PhoneNumber;
            BankAccountNumber = initializer.BankAccountNumber;

            AddDomainEvent(new UpdateCustomerDomainEvent(this.Id, this.Firstname.Value, this.Lastname.Value,
                this.Email.Value, this.PhoneNumber.Value, this.BankAccountNumber.Value, this.DateOfBirth));
        }

        public void Delete()
        {
            AddDomainEvent(new DeleteCustomerDomainEvent(this.Id));
        }
    }
}
