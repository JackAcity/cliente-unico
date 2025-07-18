using ErrorOr;
using MediatR;

namespace Application.Invitados.InvitadosEtiquetas.CreateList
{
    public record CreateInvitadoEtiquetaCommand(
        long EtiquetaId
    ) : IRequest<ErrorOr<long>>
    {
        public Guid InvitadoId { get; init; }
    }


    public record CreateListInvitadoEtiquetaCommand(
    List<CreateInvitadoEtiquetaCommand> Etiquetas
) : IRequest<ErrorOr<bool>>;
}
