
namespace Domain.Entities.Invitados.Origenes
{
    public interface IOrigenInvitadoDocumentoRepository
    {
        Task<Guid> AddAsync(OrigenInvitadoDocumento documento, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<OrigenInvitadoDocumento> documentos, CancellationToken cancellationToken);
    }
}
