using ErrorOr;
using MediatR;

namespace Application.Invitados.Documento.Create;

public record CreateDocumentoCommand(
    string TipoDocumentoCodigo,
    string DocumentoIdentidad,
    bool EsPrioridad
) : IRequest<ErrorOr<int>>
{
    public Guid InvitadoId { get; init; }
}
