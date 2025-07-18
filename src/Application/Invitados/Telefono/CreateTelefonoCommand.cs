using Domain.Entities.General;
using ErrorOr;
using MediatR;

namespace Application.Invitados.Telefono;

public record CreateTelefonoCommand(
    string TipoContactoCodigo,
    string PrefijoPais,
    string Numero,
    bool DeseaNotificacion,
    string MedioNotificacion,
    string TipoNotificacion
) : IRequest<ErrorOr<Guid>>
{
    public Guid InvitadoId { get; init; }
}
