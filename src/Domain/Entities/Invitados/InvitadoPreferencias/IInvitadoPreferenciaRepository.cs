using Domain.Entities.Invitados.EtiquetaInvitados;


namespace Domain.Entities.Invitados.InvitadoPreferencias
{
    public interface IInvitadoPreferenciaRepository
    {
        Task<long> AddAsync(InvitadoPreferencia invitadoPreferencia, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<InvitadoPreferencia> invitadoPreferencias, CancellationToken cancellationToken);
    }
}
