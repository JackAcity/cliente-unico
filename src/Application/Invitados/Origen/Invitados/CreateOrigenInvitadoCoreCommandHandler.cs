using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities.Invitados.Origenes;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Application.Invitados.Origen.Invitados
{
    public class CreateOrigenInvitadoCoreCommandHandler
        : IRequestHandler<CreateOrigenInvitadoCoreCommand, ErrorOr<Guid>>
    {
        private readonly IOrigenInvitadoRepository _repository;
        private readonly ILogger<CreateOrigenInvitadoCoreCommandHandler> _logger;

        public CreateOrigenInvitadoCoreCommandHandler(
            IOrigenInvitadoRepository repository,
            ILogger<CreateOrigenInvitadoCoreCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<Guid>> Handle(
            CreateOrigenInvitadoCoreCommand command,
            CancellationToken ct)
        {
          

            var entidad = new OrigenInvitado(
                sistemaCodigo: command.SistemaCodigo,
                origenId: command.OrigenId,
                fechaRegistro: command.FechaRegistroOrigen,
                fechaModifica: command.FechaModificaOrigen,
                invitadoId: command.InvitadoId,
                estadoId: command.EstadoId
            );

            await _repository.AddAsync(entidad, ct);

            return entidad.Id;
        }
    }
}
