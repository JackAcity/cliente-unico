
namespace Domain.Entities.Invitados.Documentos
{
    public interface IInvitadoDocumentoRepository
    {
        Task<Guid> AddAsync(InvitadoDocumento invitadoDocumento, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<InvitadoDocumento> invitadoDocumento, CancellationToken cancellationToken);
    }
}
