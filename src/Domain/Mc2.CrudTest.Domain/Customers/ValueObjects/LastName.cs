using Mc2.CrudTest.Domain.Customers.Constants;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class LastName : ValueObject
    {
        public static implicit operator LastName(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new LastName(value: value.ToLower().Trim());
        }

        private LastName() : base()
        {
        }
        private LastName(string value) : this()
        {
            Value = value;
            Validate();
        }

        public string Value { get; init; }

        private void Validate()
        {
            if (Value.Length > ConstantValues.MaximumLastnameLength)
                throw new LastNameLengthIsLongerThanLimitationException(this.Value);
        }
    }
}
