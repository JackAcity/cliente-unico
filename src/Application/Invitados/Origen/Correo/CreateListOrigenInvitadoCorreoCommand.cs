using ErrorOr;
using MediatR;

namespace Application.Invitados.Origen
{
    public record CreateListOrigenInvitadoCorreoCommand(
        List<CreateOrigenInvitadoCorreoCoreCommand> Correos
    ) : IRequest<ErrorOr<bool>>;

    public record CreateOrigenInvitadoCorreoCoreCommand(
        Guid InvitadoCorreoId,
        Guid OrigenInvitadoId,
        bool EsValidado,
        string CodigoValidacion,
        bool EsPrincipal
    );
}
