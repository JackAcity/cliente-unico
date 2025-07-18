
namespace Domain.Entities.Invitados.Telefonos
{
    public interface INotificacionTelefonoRepository
    {
        Task<long> AddAsync(TelefonoNotificacion telefono, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<TelefonoNotificacion> telefonos, CancellationToken cancellationToken);

    }
}
