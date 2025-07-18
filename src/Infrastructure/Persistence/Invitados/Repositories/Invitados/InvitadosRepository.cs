using Application.Data.Invitados;
using Domain.Entities.Invitados.Invitados;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Invitados.Repositories.Invitados;

public class InvitadoRepository : IInvitadoRepository
{
    private readonly IApplicationInvitadosDbContext _context;

    public InvitadoRepository(IApplicationInvitadosDbContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));
    }


    public async Task<Guid> AddAsync(Invitado invitado)
    {
        if (invitado != null)
        {
            var invitadoResult= await _context.Invitados.AddAsync(invitado);

            return invitado.Id;
        }
        else
        {
            throw new ArgumentNullException(nameof(invitado));
        }
    }

    public Task DeleteAsync(int id, AuditInfo auditRecord)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Invitado>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Invitado> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<(List<Invitado> Items, int TotalCount)> GetPagedAsync(int page, int size, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Invitado invitado, AuditInfo auditRecord)
    {
        throw new NotImplementedException();
    }
}