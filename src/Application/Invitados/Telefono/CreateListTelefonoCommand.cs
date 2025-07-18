using ErrorOr;
using MediatR;

namespace Application.Invitados.Telefono;

public record CreateListTelefonoCommand(
    List<CreateTelefonoCommand> Telefonos
) : IRequest<ErrorOr<bool>>;
