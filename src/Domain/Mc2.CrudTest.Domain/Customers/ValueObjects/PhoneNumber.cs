using libphonenumber;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public static implicit operator PhoneNumber(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new PhoneNumber(value: value);
        }

        private PhoneNumber() : base()
        {
        }
        private PhoneNumber(string value) : this()
        {
            Value = value;
            Validate();
        }

        public string Value { get; init; }

        private void Validate()
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.Instance;
            try
            {
                phoneUtil.Parse(Value, "IR");
            }
            catch (NumberParseException e)
            {
                throw new InvalidPhoneNumberException(ExceptionsEnum.InvalidPhoneNumberException, e.Message);
            }
        }
    }
}
