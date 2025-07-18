using ErrorOr;
using MediatR;

namespace Application.Invitados.Invitados.Create
{
    public record CreateInvitadoCommand(
        string PrimerNombre,
        string SegundoNombre,
        string ApellidoPaterno,
        string ApellidoMaterno,
        long EstadoCivilId,
        string FechaNacimiento,
        long GeneroId,
        string PaisId
    ) : IRequest<ErrorOr<Guid>>;

}
