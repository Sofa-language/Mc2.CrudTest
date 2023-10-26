using Mc2.CrudTest.Domain.Customers.Constants;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;
using PhoneNumbers;

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
            if (Value.Length > ConstantValues.MaximumPhoneNumberLength)
                throw new PhoneNumberLengthIsLongerThanLimitationException(this.Value);

            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var convertedPhoneNumber = phoneUtil.Parse(Value, null);

                var numberType = phoneUtil.GetNumberType(convertedPhoneNumber);

                if (numberType != PhoneNumberType.MOBILE && numberType != PhoneNumberType.FIXED_LINE_OR_MOBILE)
                {
                    throw new InvalidPhoneNumberException(ExceptionsEnum.InvalidPhoneNumberException, $"{Value} is not mobile numberss");
                }
            }
            catch (NumberParseException e)
            {
                throw new InvalidPhoneNumberException(ExceptionsEnum.InvalidPhoneNumberException, e.Message);
            }
        }
    }
}
