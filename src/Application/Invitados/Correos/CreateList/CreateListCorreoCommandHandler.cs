using Domain.Entities.Invitados.CorreoNotificaciones;
using Domain.Entities.Invitados.Correos;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Correos.CreateList;
public class CreateListCorreoCommandHandler : IRequestHandler<CreateListCorreoCommand, ErrorOr<bool>>
{
    private readonly IInvitadoCorreoRepository _repository;
    private readonly ICorreoNotificacionRepository _notifRepo;
    private readonly ILogger<CreateListCorreoCommand> _logger;
    public CreateListCorreoCommandHandler(
    IInvitadoCorreoRepository repository,
      ICorreoNotificacionRepository notifRepo,
    ILogger<CreateListCorreoCommand> logger)
    {
        _repository = repository;
        _notifRepo = notifRepo;
        _logger = logger;
    }

    public async Task<ErrorOr<bool>> Handle(CreateListCorreoCommand command, CancellationToken cancellationToken)
    {
        var correosConDto = command.Correos
            .Select(dto => new
            {
                Entidad = new InvitadoCorreo(
                    dto.InvitadoId,
                    dto.TipoContactoCodigo,
                    dto.Correo,
                    dto.DeseaNotificacion,
                    dto.EsPrioridad
                ),
                Dto = dto
            })
            .ToList();

        var entidades = correosConDto.Select(x => x.Entidad).ToList();
        await _repository.AddListAsync(entidades, cancellationToken);

        var notificaciones = correosConDto
            .Where(x => x.Entidad.DeseaNotificacion)
            .Select(x => new CorreoNotificacion(
                invitadoCorreoId: x.Entidad.Id,
                tipoNotifCodigo: x.Dto.TipoNotificacion
            ))
            .ToList();

        if (notificaciones.Any())
            await _notifRepo.AddListAsync(notificaciones, cancellationToken);

        return true;
    }
}
