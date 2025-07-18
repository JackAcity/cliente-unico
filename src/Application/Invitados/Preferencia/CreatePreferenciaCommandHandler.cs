using Application.Common.Exceptions;
using Application.Invitados.Preferencial;
using Application.Services;
using Domain.Entities.Invitados.InvitadoPreferencias;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Preferencia
{
    public class CreatePreferenciaCommandHandler
        : IRequestHandler<CreatePreferenciaCommand, ErrorOr<long>>
    {
        private readonly IInvitadoPreferenciaRepository _repository;
        private readonly ILogger<CreatePreferenciaCommandHandler> _logger;

        public CreatePreferenciaCommandHandler(
            IInvitadoPreferenciaRepository repository,
            ILogger<CreatePreferenciaCommandHandler> logger
        )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<long>> Handle(
            CreatePreferenciaCommand command,
            CancellationToken ct
        )
        {
            var entidad = new InvitadoPreferencia(
                categoriaId: command.CategoriaId,
                valor: command.Valor,
                leGusta: command.LeGusta,
                invitadoId: command.InvitadoId
            );

            var newId = await _repository.AddAsync(entidad, ct);

            return newId;
        }
    }
}
