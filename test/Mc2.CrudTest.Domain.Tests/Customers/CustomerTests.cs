using Mc2.CrudTest.Domain.Tests.Customers.Builders;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Tests.Customers
{
    public class CustomerTests
    {
        #region Create
        [Fact]
        public void Create_customer_successfully()
        {
            var id = 1;
            var firstname = Guid.NewGuid().ToString();
            var lastname = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var phoneNumber = Guid.NewGuid().ToString();
            var bankAccountNumber = Guid.NewGuid().ToString();
            var dateOfBirth = new DateTimeOffset(1988, 8, 9, 0, 0, 0, new TimeSpan());
            var builder = CustomerBuilder.Instance;

            var customer = builder
                .WithId(id)
                .WithFirstname(firstname)
                .WithLastname(lastname)
                .WithEmail(email)
                .WithPhoneNumber(phoneNumber)
                .WithBankAccountNumber(bankAccountNumber)
                .WithDateOfBirth(dateOfBirth)
                .Create();

            customer.ShouldNotBeNull();
            customer.Id.ShouldBe(id);
            customer.Firstname.ShouldBe(firstname);
            customer.Lastname.ShouldBe(lastname);
            customer.DateOfBirth.ShouldBe(dateOfBirth);
            customer.Email.ShouldBe(email);
            customer.PhoneNumber.ShouldBe(phoneNumber);
            customer.BankAccountNumber.ShouldBe(bankAccountNumber);
        }
        [Fact]
        public void Unable_to_create_customer_successfully_when_mobilenumber_is_invalid()
        {
            var phoneNumber = Guid.NewGuid().ToString();
            var builder = CustomerBuilder.Instance;

            Assert.Throws<Exception>(() => builder.WithPhoneNumber(phoneNumber).Create());
        }
        #endregion

        #region Update
        [Fact]
        public void Update()
        {

        }
        #endregion

        #region Delete
        [Fact]
        public void Delete()
        {

        }
        #endregion
    }
}
