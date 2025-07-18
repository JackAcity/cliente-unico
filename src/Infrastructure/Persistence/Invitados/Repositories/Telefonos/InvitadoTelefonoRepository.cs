using Application.Data.Invitados;
using Domain.Entities.Invitados.Telefonos;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.Telefonos
{
    public class InvitadoTelefonoRepository : IInvitadoTelefonoRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public InvitadoTelefonoRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(InvitadoTelefono telefono, CancellationToken cancellationToken)
        {
            if (telefono != null)
            {
                var invitadoResult = await _context.InvitadoTelefono.AddAsync(telefono);
                return telefono.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(telefono));
            }
        }

        public async Task<bool> AddListAsync(List<InvitadoTelefono> telefonos, CancellationToken cancellationToken)
        {
            if (!telefonos.IsNullOrEmpty())
            {
                await _context.InvitadoTelefono.AddRangeAsync(telefonos);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(telefonos));
            }
        }
    }
}
