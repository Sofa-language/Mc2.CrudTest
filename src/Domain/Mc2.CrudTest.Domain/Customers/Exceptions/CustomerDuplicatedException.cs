using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Domain.Customers.Exceptions
{
    public class CustomerDuplicatedException : BusinessException
    {
        public CustomerDuplicatedException(ExceptionsEnum code)
            : base(code, $"There is a customer with the same information")
        {
        }
    }
}
