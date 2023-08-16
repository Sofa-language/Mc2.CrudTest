using Mc2.CrudTest.Application.Contract.Customers.Commands;
using Mc2.CrudTest.Application.Contract.Customers.Exceptions;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DomainServices;
using Mc2.CrudTest.Domain.Customers.Initializers;
using Mc2.CrudTest.Presentation.Shared.Application;
using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;
using MediatR;

namespace Mc2.CrudTest.Application.Customers.CommandHandlers
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, long>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailAddressDuplicationValidatorService _emailAddressDuplicationService;
        private readonly ICustomerDuplicationValidatorService _customerDuplicationValidatorService;
        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<long> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var initializer = new CreateOrUpdateInitializer(request.Id, request.FirstName, request.LastName, request.PhoneNumber,
                request.Email, request.BankAccountNumber, request.DateOfBirth);

            var customer = await _customerRepository.GetAsync(request.Id, cancellationToken);
            if (customer == null)
                throw new UnableToFindCustomerException(ExceptionsEnum.UnableToFindCustomerException, request.Id.ToString());

            await customer.UpdateAsync(initializer, _emailAddressDuplicationService, _customerDuplicationValidatorService);
            await _unitOfWork.CommitAsync(cancellationToken);

            return customer.Id;
        }
    }
}
