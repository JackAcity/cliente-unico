using Application.Data.Invitados;
using Domain.Entities.Invitados.CorreoNotificaciones;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.CorreoNotificaciones
{
    public class CorreoNotificacionRepository: ICorreoNotificacionRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public CorreoNotificacionRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<long> AddAsync(CorreoNotificacion correoNotificacion)
        {
            if (correoNotificacion != null)
            {
                var invitadoResult = await _context.CorreoNotificacion.AddAsync(correoNotificacion);
                return correoNotificacion.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(correoNotificacion));
            }
        }

        public async Task<bool> AddListAsync(List<CorreoNotificacion> correos, CancellationToken cancellationToken)
        {
            if (!correos.IsNullOrEmpty())
            {
                await _context.CorreoNotificacion.AddRangeAsync(correos);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(correos));
            }
        }
    }
}
