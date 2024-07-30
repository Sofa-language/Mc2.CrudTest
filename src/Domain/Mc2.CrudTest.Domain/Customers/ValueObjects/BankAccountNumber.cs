using IbanNet;
using Mc2.CrudTest.Domain.Customers.Constants;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class BankAccountNumber : ValueObject
    {
        public static implicit operator BankAccountNumber(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new BankAccountNumber(value: value);
        }

        private BankAccountNumber() : base()
        {
        }
        private BankAccountNumber(string value) : this()
        {
            Value = value;
            Validate();
        }

        public string Value { get; init; }

        private void Validate()
        {
            if (Value.Length > ConstantValues.MaximumBankAccountNumberLength)
                throw new BankAccountNumberLengthIsLongerThanLimitationException(this.Value);

            var validator = new IbanValidator();
            ValidationResult validationResult = validator.Validate(Value);
            if (!validationResult.IsValid)
                throw new InvalidBankAccountNumberException(ExceptionsEnum.InvalidBankAccountNumberException, Value);
        }
    }
}
