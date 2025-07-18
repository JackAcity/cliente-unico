using ErrorOr;
using MediatR;

namespace Application.Invitados.Notas.Create
{
    public record CreateNotaCommand(
        string Comentario
    ) : IRequest<ErrorOr<long>>
    {
        public Guid InvitadoId { get; init; }
    }
}
