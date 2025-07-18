using ErrorOr;
using MediatR;

namespace Application.Invitados.Origen.Documento
{
    public record CreateListOrigenInvitadoDocumentoCommand(
        List<CreateOrigenInvitadoDocumentoCoreCommand> Documentos
    ) : IRequest<ErrorOr<bool>>;

    public record CreateOrigenInvitadoDocumentoCoreCommand(
        Guid InvitadoDocumentoId,
        Guid OrigenInvitadoId,
        bool EsPrincipal
    );

}
