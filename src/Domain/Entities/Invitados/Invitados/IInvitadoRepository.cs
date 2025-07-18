using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Invitados;
public interface IInvitadoRepository
{
    Task<Invitado> GetByIdAsync(int id);
    Task<IEnumerable<Invitado>> GetAllAsync();
    Task<(List<Invitado> Items, int TotalCount)> GetPagedAsync(int page, int size, CancellationToken cancellationToken);
    Task<Guid> AddAsync(Invitado invitado);
    Task UpdateAsync(Invitado invitado, AuditInfo auditRecord);
    Task DeleteAsync(int id, AuditInfo auditRecord);
}