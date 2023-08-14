using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.Initializers;

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
            WithPhoneNumber("+989224957626");
            WithEmail("jahanbin.ali1988@gmail.com");
            WithBankAccountNumber("123456789");
        }

        internal CustomerBuilder WithId(long id) { _id = id; return this; }
        internal CustomerBuilder WithFirstname(string firstname) { _firstname = firstname; return this; }
        internal CustomerBuilder WithLastname(string lastname) { _lastname = lastname; return this; }
        internal CustomerBuilder WithDateOfBirth(DateTimeOffset dateOfBirth) { _dateOfBirth = dateOfBirth; return this; }
        internal CustomerBuilder WithPhoneNumber(string phoneNumber) { _phoneNumber = phoneNumber; return this; }
        internal CustomerBuilder WithEmail(string email) { _email = email; return this; }
        internal CustomerBuilder WithBankAccountNumber(string bankAccountNumber) { _bankAccountNumber = bankAccountNumber; return this; }

        internal Customer Create()
        {
            var initializer = CreateInitializer();

            return new Customer(initializer);
        }

        private CreateOrUpdateInitializer CreateInitializer()
        {
            return new CreateOrUpdateInitializer(_id, _firstname, _lastname, _phoneNumber, _email, _bankAccountNumber, _dateOfBirth);
        }
    }
}
