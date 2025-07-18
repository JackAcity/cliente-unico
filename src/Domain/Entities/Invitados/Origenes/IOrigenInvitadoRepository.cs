
namespace Domain.Entities.Invitados.Origenes
{
    public interface IOrigenInvitadoRepository
    {
        Task<Guid> AddAsync(OrigenInvitado invitado, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<OrigenInvitado> invitados, CancellationToken cancellationToken);
    }
}
