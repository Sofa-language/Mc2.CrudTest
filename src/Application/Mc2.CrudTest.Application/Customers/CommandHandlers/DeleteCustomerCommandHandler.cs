using Mc2.CrudTest.Application.Contract.Customers.Commands;
using Mc2.CrudTest.Application.Contract.Customers.Exceptions;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Presentation.Shared.Application;
using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;
using MediatR;

namespace Mc2.CrudTest.Application.Customers.CommandHandlers
{
    public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(request.CustomerId, cancellationToken);
            if (customer == null)
                throw new UnableToFindCustomerException(ExceptionsEnum.UnableToFindCustomerException, request.CustomerId.ToString());

            customer.Delete();
            await _customerRepository.DeleteAsync(customer, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
