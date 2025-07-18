
namespace Domain.Entities.Invitados.Notas
{
    public interface IInvitadoNotaRepository
    {
        Task<long> AddAsync(InvitadoNota nota, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<InvitadoNota> notas, CancellationToken cancellationToken);
    }
}
