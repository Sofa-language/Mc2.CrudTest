using Mc2.CrudTest.Presentation.Shared.Application;

namespace Mc2.CrudTest.Application.Contract.Customers.Commands
{
    public class CreateCustomerCommand : CommandBase<long>
    {
        public CreateCustomerCommand(string firstName, string lastName, string email, string phoneNumber, string bankAccountNumber, DateTimeOffset dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            BankAccountNumber = bankAccountNumber;
            DateOfBirth = dateOfBirth;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
