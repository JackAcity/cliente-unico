using ErrorOr;
using MediatR;

namespace Application.Invitados.Direccion.Create;

public record CreateDireccionCommand(
    string TipoContactoCodigo,
    long? DistritoId,
    string TipoViaCodigo,
    string Coordenadas,
    string NumeroLote,
    int Piso,
    int Departamento,
    string Direccion,
    string Referencia,
    bool EsPrincipal,
    bool Prioridad
    
) : IRequest<ErrorOr<int>>
{
    public Guid InvitadoId { get; init; }
}
