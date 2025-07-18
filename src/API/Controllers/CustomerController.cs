using Application.Invitados.InvitadoOrchestrator.Create;
using Application.Invitados.Origen;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1/customers")]
public class CustomerController : ApiController
{
    private readonly ISender _mediator;

    public CustomerController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateInvitadoOrchestratorCommand createCustomerCommand)
    {
        var createCustomerResult = await _mediator.Send(createCustomerCommand);

         return createCustomerResult.Match(
            customerResult => Created(string.Empty, customerResult),
            errors => Problem(errors)
        );
    }

    [HttpPost("/origen")]
    public async Task<IActionResult> CreateOrigenInvitado([FromBody] CreateOrigenInvitadoCommand createCustomerCommand)
    {
        var createCustomerResult = await _mediator.Send(createCustomerCommand);

        return createCustomerResult.Match(
           customerResult => Created(string.Empty, customerResult),
           errors => Problem(errors)
       );
    }
}

