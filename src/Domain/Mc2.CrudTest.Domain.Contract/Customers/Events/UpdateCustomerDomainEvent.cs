using Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent;

namespace Mc2.CrudTest.Domain.Contract.Customers.Events
{
    public class UpdateCustomerDomainEvent : DomainEventBase
    {
        public UpdateCustomerDomainEvent(long id, string firstName, string lastName, string email, 
            string phoneNumber, string bankAccountNumber, DateTimeOffset dateOfBirth)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            BankAccountNumber = bankAccountNumber;
            DateOfBirth = dateOfBirth;
        }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
