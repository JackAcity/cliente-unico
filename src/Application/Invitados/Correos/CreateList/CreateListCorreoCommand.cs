using Application.Invitados.Correos.Create;
using ErrorOr;
using MediatR;

namespace Application.Invitados.Correos.CreateList;

public record CreateListCorreoCommand(
    List<CreateCorreoCommand> Correos
) : IRequest<ErrorOr<bool>>;