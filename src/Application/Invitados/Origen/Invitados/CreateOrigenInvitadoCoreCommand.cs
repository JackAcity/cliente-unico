using ErrorOr;
using MediatR;

namespace Application.Invitados.Origen.Invitados
{
    public record CreateOrigenInvitadoCoreCommand(
        string SistemaCodigo,
        string OrigenId,
        DateTime FechaRegistroOrigen,
        DateTime? FechaModificaOrigen,
        Guid InvitadoId,
        long? EstadoId
    ) : IRequest<ErrorOr<Guid>>;
}
