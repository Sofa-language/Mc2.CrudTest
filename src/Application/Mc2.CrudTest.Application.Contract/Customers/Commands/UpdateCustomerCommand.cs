using Mc2.CrudTest.Presentation.Shared.Application;

namespace Mc2.CrudTest.Application.Contract.Customers.Commands
{
    public class UpdateCustomerCommand : CommandBase<long>
    {
        public UpdateCustomerCommand(long id, string firstName, string lastName, string email, string phoneNumber, string bankAccountNumber, DateTimeOffset dateOfBirth)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            BankAccountNumber = bankAccountNumber;
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
