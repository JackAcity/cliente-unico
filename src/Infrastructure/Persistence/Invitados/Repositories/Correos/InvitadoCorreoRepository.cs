using Application.Data.Invitados;
using Domain.Entities.Invitados.Correos;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.Correos;

public class InvitadoCorreoRepository : IInvitadoCorreoRepository
{
    private readonly IApplicationInvitadosDbContext _context;

    public InvitadoCorreoRepository(IApplicationInvitadosDbContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));
    }

    public async Task<Guid> AddAsync(InvitadoCorreo correo, CancellationToken cancellationToken)
    {
        if (correo != null)
        {
            var invitadoResult = await _context.InvitadoCorreo.AddAsync(correo);
            return correo.Id;
        }
        else
        {
            throw new ArgumentNullException(nameof(correo));
        }
    }

    public async Task<bool> AddListAsync(List<InvitadoCorreo> correos, CancellationToken cancellationToken)
    {
        if (!correos.IsNullOrEmpty())
        {
            await _context.InvitadoCorreo.AddRangeAsync(correos);
            return true;
        }
        else
        {
            throw new ArgumentNullException(nameof(correos));
        }
    }
}