using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers
{
    public class Customer : Entity<long>, IAggregateRoot
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
