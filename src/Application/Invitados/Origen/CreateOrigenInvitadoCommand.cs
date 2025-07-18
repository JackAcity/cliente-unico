using Application.Invitados.Origen.Documento;
using Application.Invitados.Origen.OrigenAdicionales;
using ErrorOr;
using MediatR;
namespace Application.Invitados.Origen
{

    public record CreateOrigenInvitadoCommand(
        string SistemaCodigo,
        string OrigenId,
        DateTime FechaRegistroOrigen,
        DateTime? FechaModificaOrigen,
        Guid InvitadoId,
        long? EstadoId,
        List<CreateOrigenInvitadoCorreoDto>? Correos,
        List<CreateOrigenInvitadoDocumentoDto>? Documentos,
        List<CreateOrigenInvitadoTelefonoDto>? Telefonos,
        List<CreateOrigenAdicionalDto>? Adicionales
    ) : IRequest<ErrorOr<Guid>>;
}


public record CreateOrigenInvitadoCorreoDto(
    Guid InvitadoCorreoId,
    bool EsValidado,
    string CodigoValidacion,
    bool EsPrincipal
);

public record CreateOrigenInvitadoDocumentoDto(
    Guid InvitadoDocumentoId,
    bool EsPrincipal
);

public record CreateOrigenInvitadoTelefonoDto(
    Guid InvitadoTelefonoId,
    bool EsPrincipal,
    bool EsValidado,
    string CodigoValidacion
);

public record CreateOrigenAdicionalDto(
        long CampoPersonalizadoId,
        string Valor
);
