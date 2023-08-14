using Mc2.CrudTest.Domain.Customers.Initializers;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers
{
    public class Customer : Entity<long>, IAggregateRoot
    {
        private Customer() : base()
        {
            
        }
        public Customer(CreateOrUpdateInitializer initializer) : this()
        {
            Id = initializer.Id;
            Firstname = initializer.Firstname;
            Lastname = initializer.Lastname;
            DateOfBirth = initializer.DateOfBirth;
            PhoneNumber = initializer.PhoneNumber;
            Email = initializer.Email;
            BankAccountNumber = initializer.BankAccountNumber;
        }
        public long Id { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public DateTimeOffset DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string BankAccountNumber { get; private set; }
    }
}
