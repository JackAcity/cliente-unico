using Application.Invitados.Documento.Create;
using ErrorOr;
using MediatR;


namespace Application.Invitados.Documento.CreateList
{
    public record CreateListDocumentoCommand(
        List<CreateDocumentoCommand> Documentos
    ) : IRequest<ErrorOr<bool>>;

}
