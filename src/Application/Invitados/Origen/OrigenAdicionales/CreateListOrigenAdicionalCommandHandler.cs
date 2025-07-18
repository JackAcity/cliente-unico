using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities.Invitados.Origenes;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Origen.OrigenAdicionales
{
    public class CreateListOrigenAdicionalCommandHandler
        : IRequestHandler<CreateListOrigenAdicionalCommand, ErrorOr<bool>>
    {
        private readonly IOrigenAdicionalRepository _repository;
        private readonly ILogger<CreateListOrigenAdicionalCommandHandler> _logger;

        public CreateListOrigenAdicionalCommandHandler(
            IOrigenAdicionalRepository repository,
            ILogger<CreateListOrigenAdicionalCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateListOrigenAdicionalCommand command,
            CancellationToken ct)
        {
          
            var entidades = command.Adicionales
                .Select(dto => new OrigenAdicional(
                    origenInvitadoId:   dto.OrigenInvitadoId,
                    campoPersonalizadoId: dto.CampoPersonalizadoId,
                    valor: dto.Valor
                ))
                .ToList();

            await _repository.AddListAsync(entidades, ct);

            return true;
        }
    }
}
