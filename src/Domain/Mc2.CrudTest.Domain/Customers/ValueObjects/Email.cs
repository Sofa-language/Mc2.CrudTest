using EmailCore.Validation;
using Mc2.CrudTest.Domain.Customers.Constants;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class Email : ValueObject
    {
        public static implicit operator Email(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new Email(value: value);
        }

        private Email() : base()
        {
        }
        private Email(string value) : this()
        {
            Value = value;
            Validate();
        }

        public string Value { get; init; }

        private void Validate()
        {
            if (Value.Length > ConstantValues.MaximumEmailLength)
                throw new EmailLengthIsLongerThanLimitationException(this.Value);

            //var response = EmailInfo.Validation(Value);
            //if (!response.SyntaxValidationStatus)
            //    throw new InvalidEmailException(ExceptionsEnum.InvalidEmailException, Value);
        }
    }
}
