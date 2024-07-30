using Mc2.CrudTest.Application.Contract.Customers.Commands;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DomainServices;
using Mc2.CrudTest.Domain.Customers.Initializers;
using Mc2.CrudTest.Presentation.Shared.Application;
using Mc2.CrudTest.Presentation.Shared.SeedWork;
using Mc2.CrudTest.Presentation.Shared.Shared;

namespace Mc2.CrudTest.Application.Customers.CommandHandlers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, long>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailAddressDuplicationValidatorService _emailAddressDuplicationService;
        private readonly ICustomerDuplicationValidatorService _customerDuplicationValidatorService;
        private readonly IIdGenerator _idGenerator;
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository,
            IEmailAddressDuplicationValidatorService emailAddressDuplicationService,
            ICustomerDuplicationValidatorService customerDuplicationValidatorService,
            IUnitOfWork unitOfWork,
            IIdGenerator idGenerator)
        {
            this._customerRepository = customerRepository;
            this._emailAddressDuplicationService = emailAddressDuplicationService;
            this._customerDuplicationValidatorService = customerDuplicationValidatorService;
            _unitOfWork = unitOfWork;
            _idGenerator = idGenerator;
        }

        public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var initializer = new CreateOrUpdateInitializer(_idGenerator.GetNewId(), request.FirstName, request.LastName, request.PhoneNumber,
                request.Email, request.BankAccountNumber, request.DateOfBirth);
            var customer = await Customer.CreateAsync(initializer, _emailAddressDuplicationService, _customerDuplicationValidatorService);

            await _customerRepository.AddAsync(customer, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return customer.Id;
        }
    }
}
