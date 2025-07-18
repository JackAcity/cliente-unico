using Application.Data.Invitados;
using Domain.Entities.Invitados.Origenes;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.Origenes
{
    public class OrigenInvitadoTelefonoRepository: IOrigenInvitadoTelefonoRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public OrigenInvitadoTelefonoRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(OrigenInvitadoTelefono telefono, CancellationToken cancellationToken)
        {
            if (telefono != null)
            {
                var invitadoResult = await _context.OrigenInvitadoTelefono.AddAsync(telefono);
                return telefono.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(telefono));
            }
        }

        public async Task<bool> AddListAsync(List<OrigenInvitadoTelefono> telefonos, CancellationToken cancellationToken)
        {
            if (!telefonos.IsNullOrEmpty())
            {
                await _context.OrigenInvitadoTelefono.AddRangeAsync(telefonos);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(telefonos));
            }
        }
    }
}
