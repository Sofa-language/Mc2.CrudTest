using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.Exceptions
{
    public class PhoneNumberLengthIsLongerThanLimitationException : BusinessException
    {
        public PhoneNumberLengthIsLongerThanLimitationException(string phoneNumberValue)
            : base(code: ExceptionsEnum.PhoneNumberLengthIsLongerThanLimitationException,
                  message: $"The given value {phoneNumberValue} is longger than expected")
        {
        }
    }
}
