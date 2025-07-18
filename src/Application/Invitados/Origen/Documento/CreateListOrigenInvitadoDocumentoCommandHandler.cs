using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities.Invitados.Origenes;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Origen.Documento
{
    public class CreateListOrigenInvitadoDocumentoCommandHandler
        : IRequestHandler<CreateListOrigenInvitadoDocumentoCommand, ErrorOr<bool>>
    {
        private readonly IOrigenInvitadoDocumentoRepository _repository;
        private readonly ILogger<CreateListOrigenInvitadoDocumentoCommandHandler> _logger;

        public CreateListOrigenInvitadoDocumentoCommandHandler(
            IOrigenInvitadoDocumentoRepository repository,
            ILogger<CreateListOrigenInvitadoDocumentoCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateListOrigenInvitadoDocumentoCommand command,
            CancellationToken ct)
        {
         

            var entidades = command.Documentos
                .Select(dto => new OrigenInvitadoDocumento(
                    invitadoDocumentoId: dto.InvitadoDocumentoId,
                    origenInvitadoId:     dto.OrigenInvitadoId,
                    esPrincipal: dto.EsPrincipal
                ))
                .ToList();

            await _repository.AddListAsync(entidades, ct);
            return true;
        }
    }
}
