using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities.Invitados.Origenes;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Origen
{
    public class CreateListOrigenInvitadoTelefonoCommandHandler
        : IRequestHandler<CreateListOrigenInvitadoTelefonoCommand, ErrorOr<bool>>
    {
        private readonly IOrigenInvitadoTelefonoRepository _repository;
        private readonly ILogger<CreateListOrigenInvitadoTelefonoCommandHandler> _logger;

        public CreateListOrigenInvitadoTelefonoCommandHandler(
            IOrigenInvitadoTelefonoRepository repository,
            ILogger<CreateListOrigenInvitadoTelefonoCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateListOrigenInvitadoTelefonoCommand command,
            CancellationToken ct)
        {

            var entidades = command.Telefonos
                .Select(dto => new OrigenInvitadoTelefono(
                    invitadoTelefonoId: dto.InvitadoTelefonoId,
                    origenInvitadoId:     dto.OrigenInvitadoId,
                    esPrincipal: dto.EsPrincipal,
                    esValidado: dto.EsValidado,
                    codigoValidacion: dto.CodigoValidacion
                ))
                .ToList();

            await _repository.AddListAsync(entidades, ct);

            return true;
        }
    }
}
