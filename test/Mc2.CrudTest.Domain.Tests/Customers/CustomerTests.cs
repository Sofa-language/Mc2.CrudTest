using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Domain.Tests.Customers.Builders;
using Shouldly;

namespace Mc2.CrudTest.Domain.Tests.Customers
{
    public class CustomerTests
    {
        #region Create
        [Fact]
        public async void Create_customer_successfully()
        {
            var id = 1;
            var firstname = Guid.NewGuid().ToString();
            var lastname = Guid.NewGuid().ToString();
            var email = "jahanbin.ali1988@gmail.com";
            var phoneNumber = "09224957626";
            var bankAccountNumber = "NL91ABNA0417164300";
            var dateOfBirth = new DateTimeOffset(1988, 8, 9, 0, 0, 0, new TimeSpan());
            var builder = CustomerBuilder.Instance;

            var customer = await builder
                .WithId(id)
                .WithFirstname(firstname)
                .WithLastname(lastname)
                .WithEmail(email)
                .WithPhoneNumber(phoneNumber)
                .WithBankAccountNumber(bankAccountNumber)
                .WithDateOfBirth(dateOfBirth)
                .CreateAsync();

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
        public async Task Unable_to_create_customer_successfully_when_PhoneNumber_is_invalid()
        {
            var phoneNumber = Guid.NewGuid().ToString();
            var builder = CustomerBuilder.Instance;

            await Assert.ThrowsAsync<InvalidPhoneNumberException>(() => builder.WithPhoneNumber(phoneNumber).CreateAsync());
        }
        [Fact]
        public async Task Unable_to_create_customer_successfully_when_Email_is_invalid()
        {
            var email = Guid.NewGuid().ToString();
            var builder = CustomerBuilder.Instance;

            await Assert.ThrowsAsync<InvalidEmailException>(() => builder.WithEmail(email).CreateAsync());
        }
        [Fact]
        public async Task Unable_to_create_customer_successfully_when_BankAccountNumber_is_invalid()
        {
            var bankAccountNumber = Guid.NewGuid().ToString();
            var builder = CustomerBuilder.Instance;

            await Assert.ThrowsAsync<InvalidBankAccountNumberException>(() => builder.WithBankAccountNumber(bankAccountNumber).CreateAsync());
        }
        [Fact]
        public async Task Unable_to_create_customer_successfully_when_Email_is_not_unique()
        {
            var email = "jahanbin.ali1988@gmail.com";
            var builder = CustomerBuilder.Instance;

            await Assert.ThrowsAsync<DuplicatedEmailException>(() => builder.SetEmailAddressDuplicationService(false).WithEmail(email).CreateAsync());
        }
        [Fact]
        public async Task Unable_to_create_customer_successfully_when_customer_is_not_unique()
        {
            var firstname = Guid.NewGuid().ToString();
            var lastname = Guid.NewGuid().ToString();
            var dateOfBirth = new DateTimeOffset(1988, 8, 9, 0, 0, 0, new TimeSpan());
            var builder = CustomerBuilder.Instance;

            await Assert.ThrowsAsync<Exception>(() => 
                    builder.WithFirstname(firstname)
                    .WithLastname(lastname)
                    .WithDateOfBirth(dateOfBirth)
                    .CreateAsync()
                );
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
