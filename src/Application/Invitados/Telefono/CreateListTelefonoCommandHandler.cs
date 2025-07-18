using Domain.Entities.Invitados.Telefonos;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Telefono
{
    public class CreateListTelefonoCommandHandler : IRequestHandler<CreateListTelefonoCommand, ErrorOr<bool>>
    {
        private readonly IInvitadoTelefonoRepository _repository;
        private readonly INotificacionTelefonoRepository _notifRepo;
        private readonly ILogger<CreateListTelefonoCommand> _logger;

        public CreateListTelefonoCommandHandler(
            IInvitadoTelefonoRepository repository,
            INotificacionTelefonoRepository notifRepo,
            ILogger<CreateListTelefonoCommand> logger)
        {
            _repository = repository;
            _notifRepo = notifRepo;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(CreateListTelefonoCommand command, CancellationToken cancellationToken)
        {
            var telefonosConDto = command.Telefonos
            .Select(dto => new
            {
                Entidad = new InvitadoTelefono(
                    dto.InvitadoId,
                    dto.TipoContactoCodigo,
                    dto.PrefijoPais,
                    dto.Numero,
                    dto.DeseaNotificacion
                ),
                Dto = dto
            })
            .ToList();

            var entidades = telefonosConDto.Select(x => x.Entidad).ToList();
            await _repository.AddListAsync(entidades, cancellationToken);

            var notificaciones = telefonosConDto
                .Where(x => x.Entidad.DeseaNotificacion)
                .Select(x => new TelefonoNotificacion(
                    invitadoTelefonoId: x.Entidad.Id,
                    medioNotifCodigo: x.Dto.MedioNotificacion,
                    tipoNotifCodigo: x.Dto.TipoNotificacion
                ))
                .ToList();

            if (notificaciones.Any())
                await _notifRepo.AddListAsync(notificaciones, cancellationToken);

            return true;
        }
    }
}
