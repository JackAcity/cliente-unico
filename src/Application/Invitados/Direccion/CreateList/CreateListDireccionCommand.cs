using Application.Invitados.Direccion.Create;
using ErrorOr;
using MediatR;

namespace Application.Invitados.Direccion.CreateList
{
    public record CreateListDireccionCommand(
        List<CreateDireccionCommand> Direcciones
    ) : IRequest<ErrorOr<bool>>;
}
