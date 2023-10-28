using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.Exceptions
{
    public class LastNameLengthIsLongerThanLimitationException : BusinessException
    {
        public LastNameLengthIsLongerThanLimitationException(string lastname)
            : base(code: ExceptionsEnum.LastNameLengthIsLongerThanLimitationException,
                  message: $"The given value {lastname} is longger than expected")
        {
        }
    }
}
