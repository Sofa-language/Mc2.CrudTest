using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.Exceptions
{
    public class FirstNameLengthIsLongerThanLimitationException : BusinessException
    {
        public FirstNameLengthIsLongerThanLimitationException(string firstname)
            : base(code: ExceptionsEnum.FirstNameLengthIsLongerThanLimitationException,
                  message: $"The given value {firstname} is longger than expected")
        {
        }
    }
}
