using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities.Invitados.Origenes;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Origen
{
    public class CreateListOrigenInvitadoCorreoCommandHandler
        : IRequestHandler<CreateListOrigenInvitadoCorreoCommand, ErrorOr<bool>>
    {
        private readonly IOrigenInvitadoCorreoRepository _repository;
        private readonly ILogger<CreateListOrigenInvitadoCorreoCommandHandler> _logger;

        public CreateListOrigenInvitadoCorreoCommandHandler(
            IOrigenInvitadoCorreoRepository repository,
            ILogger<CreateListOrigenInvitadoCorreoCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateListOrigenInvitadoCorreoCommand command,
            CancellationToken ct)
        {

            var entidades = command.Correos
                .Select(dto => new OrigenInvitadoCorreo(
                    invitadoCorreoId: dto.InvitadoCorreoId,
                    origenInvitadoId:  dto.OrigenInvitadoId,
                    esValidado: dto.EsValidado,
                    codigoValidacion: dto.CodigoValidacion,
                    esPrincipal: dto.EsPrincipal
                ))
                .ToList();

            await _repository.AddListAsync(entidades, ct);
            return true;
        }
    }
}
