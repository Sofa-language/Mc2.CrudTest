using Mc2.CrudTest.Application.Contract.Customers.Commands;
using Mc2.CrudTest.Application.Contract.Customers.Dtos;
using Mc2.CrudTest.Application.Contract.Customers.Queries;
using Mc2.CrudTest.Presentation.Server.Controllers.Models;
using Mc2.CrudTest.Presentation.Shared;
using Mc2.CrudTest.Presentation.Shared.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;
        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<long> CreateAsync([FromQuery] CreateOrUpdateCustomerModel model, CancellationToken cancellationToken)
        {
            var query = new CreateCustomerCommand(model.FirstName, model.LastName, model.Email,
                model.PhoneNumber, model.BankAccountNumber, model.DateOfBirth);

            var id = await _mediator.Send(query, cancellationToken);

            return id;
        }

        [HttpPut("{customerId}")]
        public async Task<long> UpdateAsync([FromRoute]long customerId, [FromQuery] CreateOrUpdateCustomerModel model, CancellationToken cancellationToken)
        {
            var query = new UpdateCustomerCommand(customerId, model.FirstName, model.LastName, model.Email,
                model.PhoneNumber, model.BankAccountNumber, model.DateOfBirth);

            var id = await _mediator.Send(query, cancellationToken);

            return id;
        }

        [HttpDelete("{customerId}")]
        public async Task UpdateAsync([FromRoute] long customerId, CancellationToken cancellationToken)
        {
            var query = new DeleteCustomerCommand(customerId);

            await _mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        public async Task<Pagination<CustomerDto>> GetByIdAsync([FromQuery] GetAllCustomerModel getAllCustomerModel,CancellationToken cancellationToken)
        {
            var query = new GetCustomersQuery(getAllCustomerModel.PageSize, getAllCustomerModel.PageCount);

            var customers = await _mediator.Send(query, cancellationToken);

            return customers;
        }

        [HttpGet("{customerId}")]
        public async Task<Pagination<CustomerDto>> GetAsync([FromRoute]long customerId, CancellationToken cancellationToken)
        {
            var query = new GetCustomersQuery(25, 1);

            var customers = await _mediator.Send(query, cancellationToken);

            return customers;
        }
    }
}