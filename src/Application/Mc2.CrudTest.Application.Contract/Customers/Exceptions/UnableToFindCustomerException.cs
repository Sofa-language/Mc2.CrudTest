using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;

namespace Mc2.CrudTest.Application.Contract.Customers.Exceptions
{
    public class UnableToFindCustomerException : BusinessException
    {
        public UnableToFindCustomerException(ExceptionsEnum code, string message) 
            : base(code, $"Unable to find customer with the given id {message}")
        {
        }
    }
}
