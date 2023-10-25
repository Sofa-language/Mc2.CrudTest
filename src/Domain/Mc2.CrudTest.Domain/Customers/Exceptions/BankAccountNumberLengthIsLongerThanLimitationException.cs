using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.Exceptions
{
    public class BankAccountNumberLengthIsLongerThanLimitationException : BusinessException
    {
        public BankAccountNumberLengthIsLongerThanLimitationException(string bankAccountNumberValue)
            : base(code: ExceptionsEnum.BankAccountNumberLengthIsLongerThanLimitationException, 
                  message: $"The given value {bankAccountNumberValue} is longger than expected")
        {
        }
    }
}
