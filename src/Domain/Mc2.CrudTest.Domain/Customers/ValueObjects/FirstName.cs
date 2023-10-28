using Mc2.CrudTest.Domain.Customers.Constants;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class FirstName : ValueObject
    {
        public static implicit operator FirstName(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new FirstName(value: value.ToLower().Trim());
        }

        private FirstName() : base()
        {
        }
        private FirstName(string value) : this()
        {
            Value = value;
            Validate();
        }

        public string Value { get; init; }

        private void Validate()
        {
            if (Value.Length > ConstantValues.MaximumFirstnameLength)
                throw new FirstNameLengthIsLongerThanLimitationException(this.Value);
        }
    }
}
