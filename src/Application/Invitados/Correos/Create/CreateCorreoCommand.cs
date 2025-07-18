using ErrorOr;
using MediatR;

namespace Application.Invitados.Correos.Create;

public record CreateCorreoCommand(
    string TipoContactoCodigo,
    string Correo,
    bool DeseaNotificacion,
    bool EsPrioridad,
    string TipoNotificacion
) : IRequest<ErrorOr<Guid>>
{
    public Guid InvitadoId { get; set; }
}

