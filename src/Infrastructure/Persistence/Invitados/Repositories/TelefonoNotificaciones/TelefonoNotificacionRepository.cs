using Application.Data.Invitados;
using Domain.Entities.Invitados.Telefonos;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.TelefonoNotificaciones
{
    public class TelefonoNotificacionRepository : INotificacionTelefonoRepository
    {
        private readonly IApplicationInvitadosDbContext _context;
        public TelefonoNotificacionRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<long> AddAsync(TelefonoNotificacion telefono, CancellationToken cancellationToken)
        {
            if (telefono != null)
            {
                var invitadoResult = await _context.TelefonoNotificacion.AddAsync(telefono);
                return telefono.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(telefono));
            }
        }

        public async Task<bool> AddListAsync(List<TelefonoNotificacion> telefonos, CancellationToken cancellationToken)
        {
            if (!telefonos.IsNullOrEmpty())
            {
                await _context.TelefonoNotificacion.AddRangeAsync(telefonos);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(telefonos));
            }
        }
    }
}
