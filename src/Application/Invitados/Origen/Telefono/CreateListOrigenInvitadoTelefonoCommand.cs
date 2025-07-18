using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invitados.Origen
{
    public record CreateListOrigenInvitadoTelefonoCommand(
        List<CreateOrigenInvitadoTelefonoCommand> Telefonos
    ) : IRequest<ErrorOr<bool>>;

    public record CreateOrigenInvitadoTelefonoCommand(
        Guid InvitadoTelefonoId,
        Guid OrigenInvitadoId,
        bool EsPrincipal,
        bool EsValidado,
        string CodigoValidacion
    );
}
