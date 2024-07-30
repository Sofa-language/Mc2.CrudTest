using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.Exceptions
{
    public class EmailLengthIsLongerThanLimitationException : BusinessException
    {
        public EmailLengthIsLongerThanLimitationException(string emailValue) 
            : base(code: ExceptionsEnum.EmailLengthIsLongerThanLimitationException, 
                  message: $"The given value {emailValue} is longger than expected")
        {
        }
    }
}
