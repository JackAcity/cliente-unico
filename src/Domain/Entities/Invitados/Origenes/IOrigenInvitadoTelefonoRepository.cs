using Domain.Entities.Invitados.Correos;

namespace Domain.Entities.Invitados.Origenes
{
    public interface IOrigenInvitadoTelefonoRepository
    {
        Task<Guid> AddAsync(OrigenInvitadoTelefono telefono, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<OrigenInvitadoTelefono> telefonos, CancellationToken cancellationToken);
    }
}
