namespace Domain.Entities.Invitados.EtiquetaInvitados
{
    public interface IInvitadoEtiquetaRepository
    {
        Task<long> AddAsync(InvitadoEtiqueta invitadoEtiqueta, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<InvitadoEtiqueta> invitadoEtiqueta, CancellationToken cancellationToken);
    }
}
