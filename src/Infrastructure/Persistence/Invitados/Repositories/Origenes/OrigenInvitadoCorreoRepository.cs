using Application.Data.Invitados;
using Domain.Entities.Invitados.Origenes;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.Origenes
{
    public class OrigenInvitadoCorreoRepository : IOrigenInvitadoCorreoRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public OrigenInvitadoCorreoRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(OrigenInvitadoCorreo correo, CancellationToken cancellationToken)
        {
            if (correo != null)
            {
                var invitadoResult = await _context.OrigenInvitadoCorreo.AddAsync(correo);
                return correo.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(correo));
            }
        }

        public async Task<bool> AddListAsync(List<OrigenInvitadoCorreo> correos, CancellationToken cancellationToken)
        {
            if (!correos.IsNullOrEmpty())
            {
                await _context.OrigenInvitadoCorreo.AddRangeAsync(correos);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(correos));
            }
        }
    }
}
