
namespace Domain.Entities.Invitados.Direcciones
{
    public interface IInvitadoDireccionRepository
    {
        Task<Guid> AddAsync(InvitadoDireccion invitadoDireccion, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<InvitadoDireccion> invitadoDireccion, CancellationToken cancellationToken);
    }
}
