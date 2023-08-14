using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.Exceptions
{
    public class InvalidBankAccountNumberException : BusinessException
    {
        public InvalidBankAccountNumberException(ExceptionsEnum code, string message)
            : base(code, $"'{message}' is invalid for Bank account number")
        {
        }
    }
}
