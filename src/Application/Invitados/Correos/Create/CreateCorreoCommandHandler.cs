using Domain.Entities.Invitados.Correos;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Correos.Create;
public class CreateCorreoCommandHandler : IRequestHandler<CreateCorreoCommand, ErrorOr<Guid>>
{
    private readonly IInvitadoCorreoRepository _repository;
    private readonly ILogger<CreateCorreoCommand> _logger;
    public CreateCorreoCommandHandler(
        IInvitadoCorreoRepository repository,
        ILogger<CreateCorreoCommand> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateCorreoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var correo = new InvitadoCorreo(
                command.InvitadoId, 
                command.TipoContactoCodigo,
                command.Correo,
                command.DeseaNotificacion,
                command.EsPrioridad
            );

            await _repository.AddAsync(correo, cancellationToken);
            return correo.Id;
        }
        catch (Exception ex)
        {
            string errorMessage = $"Error creating DatosPrincipales: {ex.Message}";
            _logger.LogError(errorMessage);

            return Error.Failure(
                code: "DatosPrincipales.Create.Failure",
                description: errorMessage
            );
        }
    }
}
