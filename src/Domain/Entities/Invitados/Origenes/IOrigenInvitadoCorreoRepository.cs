

namespace Domain.Entities.Invitados.Origenes
{
    public interface IOrigenInvitadoCorreoRepository
    {
        Task<Guid> AddAsync(OrigenInvitadoCorreo correo, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<OrigenInvitadoCorreo> correos, CancellationToken cancellationToken);
    }
}
