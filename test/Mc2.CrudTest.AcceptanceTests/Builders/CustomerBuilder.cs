using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.Initializers;

namespace Mc2.CrudTest.AcceptanceTests.Builders
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
            
        }

        internal CustomerBuilder WithId(long id) { this._id = id; return this; }
        internal CustomerBuilder WithFirstname(string firstname) { this._firstname = firstname; return this; }
        internal CustomerBuilder WithLastname(string lastname) { this._lastname = lastname; return this; }
        internal CustomerBuilder WithDateOfBirth(DateTimeOffset dateOfBirth) { this._dateOfBirth = dateOfBirth; return this; }
        internal CustomerBuilder WithPhoneNumber(string phoneNumber) { this._phoneNumber = phoneNumber; return this; }
        internal CustomerBuilder WithEmail (string email) { this._email = email; return this; }
        internal CustomerBuilder WithBankAccountNumber(string bankAccountNumber) { this._bankAccountNumber = bankAccountNumber; return this; }

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
