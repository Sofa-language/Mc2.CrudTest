using Mc2.CrudTest.Domain.Contract.Customers.Events;
using Mc2.CrudTest.Domain.Customers.DomainServices;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.Tests.Customers.Builders;
using Shouldly;

namespace Mc2.CrudTest.Domain.Tests.Customers
{
    public class CustomerTests
    {
        #region Create
        [Theory]
        [InlineData("+989121234567")]
        [InlineData("+16156381234")]
        public async Task Create_customer_successfully(string phoneNumber)
        {
            var id = 1;
            var firstname = Guid.NewGuid().ToString();
            var lastname = Guid.NewGuid().ToString();
            var email = "jahanbin.ali1988@gmail.com";
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
            customer.Email.Value.ShouldBe(email);
            customer.PhoneNumber.ShouldBe(phoneNumber);
            customer.BankAccountNumber.ShouldBe(bankAccountNumber);
            customer.DomainEvents.Count.ShouldBe(1);
            customer.DomainEvents.Count(c=> c.GetType().Name.Equals(nameof(CreateCustomerDomainEvent))).ShouldBe(1);
        }
        [Theory]
        [InlineData("5d3ed193-79e4-4b55-ab84-9fc97746ce8d")]
        [InlineData("982188776655")]
        public async Task Unable_to_create_customer_successfully_when_PhoneNumber_is_invalid(string phoneNumber)
        {
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

            await Assert.ThrowsAsync<CustomerDuplicatedException>(() => 
                    builder.WithFirstname(firstname)
                    .WithLastname(lastname)
                    .WithDateOfBirth(dateOfBirth)
                    .SetCustomerDuplicationValidatorService(false)
                    .CreateAsync()
                );
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_customer_successfully()
        {
            var expectedFirstname = Guid.NewGuid().ToString();
            var expectedLastname = Guid.NewGuid().ToString();
            var expectedEmail = "jahanbinali1988@yahoo.com";
            var expectedPhoneNumber = "09165770704";
            var expectedBankAccountNumber = "NL91ABNA0417164300";
            var expectedDateOfBirth = new DateTimeOffset(1988, 9, 8, 0, 0, 0, new TimeSpan());
            var builder = CustomerBuilder.Instance;
            var customer = await builder.CreateAsync();
            var initializer = builder.WithFirstname(expectedFirstname).WithLastname(expectedLastname).WithEmail(expectedEmail).WithPhoneNumber(expectedPhoneNumber).WithBankAccountNumber(expectedBankAccountNumber).WithDateOfBirth(expectedDateOfBirth).CreateInitializer();

            await customer.UpdateAsync(initializer, 
                builder._emailAddressDuplicationService.Object, builder._customerDuplicationValidatorService.Object);

            customer.Firstname.ShouldBe(expectedFirstname);
            customer.Lastname.ShouldBe(expectedLastname);
            customer.DateOfBirth.ShouldBe(expectedDateOfBirth);
            customer.Email.Value.ShouldBe(expectedEmail);
            customer.PhoneNumber.ShouldBe(expectedPhoneNumber);
            customer.BankAccountNumber.ShouldBe(expectedBankAccountNumber);
            customer.DomainEvents.Count.ShouldBe(2);
            customer.DomainEvents.Count(c => c.GetType().Name.Equals(nameof(UpdateCustomerDomainEvent))).ShouldBe(1);
        }
        [Fact]
        public async Task Unable_to_Update_customer_successfully_when_email_is_duplicated()
        {
            var expectedFirstname = Guid.NewGuid().ToString();
            var expectedLastname = Guid.NewGuid().ToString();
            var expectedEmail = "jahanbin.ali1988@gmail.com";
            var expectedPhoneNumber = "09165770704";
            var expectedBankAccountNumber = "NL91ABNA0417164300";
            var expectedDateOfBirth = new DateTimeOffset(1988, 9, 8, 0, 0, 0, new TimeSpan());
            var builder = CustomerBuilder.Instance;
            var customer = await builder.CreateAsync();
            var initializer = builder.WithFirstname(expectedFirstname).WithLastname(expectedLastname).WithEmail(expectedEmail).WithPhoneNumber(expectedPhoneNumber).WithBankAccountNumber(expectedBankAccountNumber).WithDateOfBirth(expectedDateOfBirth).CreateInitializer();

            builder.SetEmailAddressDuplicationService(false);
            await Assert.ThrowsAsync<DuplicatedEmailException>(() => customer.UpdateAsync
                (initializer,
                builder._emailAddressDuplicationService.Object, builder._customerDuplicationValidatorService.Object));
        }
        [Fact]
        public async Task Unable_to_Update_customer_successfully_when_customer_is_duplicated()
        {
            var expectedFirstname = Guid.NewGuid().ToString();
            var expectedLastname = Guid.NewGuid().ToString();
            var expectedEmail = "jahanbinali88@yahoo.com";
            var expectedPhoneNumber = "09224957626";
            var expectedBankAccountNumber = "NL91ABNA0417164300";
            var expectedDateOfBirth = new DateTimeOffset(1988, 9, 8, 0, 0, 0, new TimeSpan());
            var builder = CustomerBuilder.Instance;
            var customer = await builder.CreateAsync();
            var initializer = builder.WithFirstname(expectedFirstname).WithLastname(expectedLastname).WithEmail(expectedEmail).WithPhoneNumber(expectedPhoneNumber).WithBankAccountNumber(expectedBankAccountNumber).WithDateOfBirth(expectedDateOfBirth).CreateInitializer();

            builder.SetCustomerDuplicationValidatorService(false);
            await Assert.ThrowsAsync<CustomerDuplicatedException>(() => customer.UpdateAsync
                (initializer,
                builder._emailAddressDuplicationService.Object, builder._customerDuplicationValidatorService.Object));
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_Customer_successfully()
        {
            var builder = CustomerBuilder.Instance;
            var customer = await builder.CreateAsync();

            customer.Delete();

            customer.DomainEvents.Count.ShouldBe(2);
            customer.DomainEvents.Count(c => c.GetType().Name.Equals(nameof(DeleteCustomerDomainEvent))).ShouldBe(1);
        }
        #endregion
    }
}
