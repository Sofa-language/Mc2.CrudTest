namespace Mc2.CrudTest.Domain.Customers.Initializers
{
    public class CreateOrUpdateInitializer
    {
        public CreateOrUpdateInitializer(long id, string firstname, 
            string lastname, string phoneNumber, string email,
            string bankAccountNumber, DateTimeOffset dateOfBirth)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
            DateOfBirth = dateOfBirth;
        }
        public DateTimeOffset DateOfBirth { get; internal set; }
        public long Id { get; internal set; }
        public string Firstname { get; internal set; }
        public string Lastname { get; internal set; }
        public string PhoneNumber { get; internal set; }
        public string Email { get; internal set; }
        public string BankAccountNumber { get; internal set; }
    }
}
