using ErrorOr;
using MediatR;

namespace Application.Invitados.Origen.OrigenAdicionales
{
    public record CreateListOrigenAdicionalCommand(
        List<CreateOrigenAdicionalCommand> Adicionales
    ) : IRequest<ErrorOr<bool>>;

    public record CreateOrigenAdicionalCommand(
        Guid OrigenInvitadoId,
        long CampoPersonalizadoId,
        string Valor
    );
}
