using Application.Invitados.Notas.Create;
using ErrorOr;
using MediatR;

namespace Application.Invitados.Notas.CreateList
{
    public record CreateListNotaCommand(
      List<CreateNotaCommand> Notas
  ) : IRequest<ErrorOr<bool>>;
    
}
