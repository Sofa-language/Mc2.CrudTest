using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Contract.Customers.Dtos
{
    public class CustomerDto
    {
        public CustomerDto(long id, string firstname, string lastname, string phoneNumber, string email, string bankAccountNumber, DateTimeOffset dateOfBirth)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
            DateOfBirth = dateOfBirth;
        }
        public long Id { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public DateTimeOffset DateOfBirth { get; init; }
        public string PhoneNumber { get; init; }
        public string Email { get; init; }
        public string BankAccountNumber { get; init; }
    }
}
