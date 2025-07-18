using ErrorOr;
using MediatR;

namespace Application.Invitados.Preferencial;

public record CreatePreferenciaCommand(
    long CategoriaId,
    string Valor,
    bool LeGusta
) : IRequest<ErrorOr<long>>
{
    public Guid InvitadoId { get; init; }
}


public record CreateListPreferenciaCommand(
    List<CreatePreferenciaCommand> Preferencias
) : IRequest<ErrorOr<bool>>;