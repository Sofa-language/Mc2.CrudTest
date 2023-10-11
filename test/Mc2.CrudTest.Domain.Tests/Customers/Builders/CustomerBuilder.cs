using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DomainServices;
using Mc2.CrudTest.Domain.Customers.Initializers;
using Moq;

namespace Mc2.CrudTest.Domain.Tests.Customers.Builders
{
    public class CustomerBuilder
    {
        internal long _id;
        internal string _firstname;
        internal string _lastname;
        internal DateTimeOffset _dateOfBirth;
        internal string _phoneNumber;
        internal string _email;
        internal string _bankAccountNumber;

        internal Mock<IEmailAddressDuplicationValidatorService> _emailAddressDuplicationService;
        internal Mock<ICustomerDuplicationValidatorService> _customerDuplicationValidatorService;
        
        internal static CustomerBuilder Instance
        {
            get
            {
                var result = new CustomerBuilder();

                return result;
            }
        }

        public CustomerBuilder()
        {

            WithId(1);
            WithFirstname("Ali");
            WithLastname("Jahanbin");
            WithDateOfBirth(new DateTimeOffset(1988, 8, 9, 0, 0,0 , new TimeSpan()));
            WithPhoneNumber("+989121234567");
            WithEmail("jahanbin.ali1988@gmail.com");
            WithBankAccountNumber("NL91ABNA0417164300");

            _emailAddressDuplicationService = new Mock<IEmailAddressDuplicationValidatorService>();
            _customerDuplicationValidatorService = new Mock<ICustomerDuplicationValidatorService>();

            SetEmailAddressDuplicationService(true);
            SetCustomerDuplicationValidatorService(true);
        }

        internal CustomerBuilder WithId(long id) { _id = id; return this; }
        internal CustomerBuilder WithFirstname(string firstname) { _firstname = firstname; return this; }
        internal CustomerBuilder WithLastname(string lastname) { _lastname = lastname; return this; }
        internal CustomerBuilder WithDateOfBirth(DateTimeOffset dateOfBirth) { _dateOfBirth = dateOfBirth; return this; }
        internal CustomerBuilder WithPhoneNumber(string phoneNumber) { _phoneNumber = phoneNumber; return this; }
        internal CustomerBuilder WithEmail(string email) { _email = email; return this; }
        internal CustomerBuilder WithBankAccountNumber(string bankAccountNumber) { _bankAccountNumber = bankAccountNumber; return this; }

        internal CustomerBuilder SetEmailAddressDuplicationService(bool result)
        {
            _emailAddressDuplicationService.Setup(s=> s.IsValidAsync(It.IsAny<long>(), It.IsAny<string>())).ReturnsAsync(result);

            return this;
        }
        internal CustomerBuilder SetCustomerDuplicationValidatorService(bool result)
        {
            _customerDuplicationValidatorService.Setup(s => s.IsValidAsync(It.IsAny<long?>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTimeOffset>())).ReturnsAsync(result);

            return this;
        }


        internal async Task<Customer> CreateAsync()
        {
            var initializer = CreateInitializer();

            return await Customer.CreateAsync(initializer, _emailAddressDuplicationService.Object, _customerDuplicationValidatorService.Object);
        }

        internal CreateOrUpdateInitializer CreateInitializer()
        {
            return new CreateOrUpdateInitializer(_id, _firstname, _lastname, _phoneNumber, _email, _bankAccountNumber, _dateOfBirth);
        }
    }
}
