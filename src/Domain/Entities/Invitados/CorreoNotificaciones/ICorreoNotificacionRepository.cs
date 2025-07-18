
using Domain.Entities.Invitados.Correos;

namespace Domain.Entities.Invitados.CorreoNotificaciones
{
    public interface ICorreoNotificacionRepository
    {
        Task<long> AddAsync(CorreoNotificacion correoNotificacion);
        Task<bool> AddListAsync(List<CorreoNotificacion> correos, CancellationToken cancellationToken);
    }
}
