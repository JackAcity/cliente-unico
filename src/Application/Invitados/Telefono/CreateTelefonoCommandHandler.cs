using Application.Services;
using Domain.Entities.Invitados.Telefonos;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Telefono
{
    public class CreateTelefonoCommandHandler : IRequestHandler<CreateTelefonoCommand, ErrorOr<Guid>>
    {
        private readonly IInvitadoTelefonoRepository _repository;
        private readonly ILogger<CreateTelefonoCommand> _logger;

        public CreateTelefonoCommandHandler(
          IInvitadoTelefonoRepository repository,
          ILogger<CreateTelefonoCommand> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateTelefonoCommand command, CancellationToken cancellationToken)
        {
                var telefono = new InvitadoTelefono(
                    command.InvitadoId,
                    command.TipoContactoCodigo,
                    command.PrefijoPais,
                    command.Numero,
                    command.DeseaNotificacion
                );

                await _repository.AddAsync(telefono, cancellationToken);
                return telefono.Id;
        }
    }
}
