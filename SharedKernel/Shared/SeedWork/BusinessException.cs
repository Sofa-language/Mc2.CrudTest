using Mc2.CrudTest.Presentation.Shared.Exceptions;

namespace Mc2.CrudTest.Presentation.Shared.SeedWork
{
    public abstract class BusinessException : Exception
    {
        public BusinessException(ExceptionsEnum code, string message)
        {
            Code = code;
            Message = message;
        }
        public ExceptionsEnum Code { get; private set; }
        public string Message { get; private set; }
    }
}
