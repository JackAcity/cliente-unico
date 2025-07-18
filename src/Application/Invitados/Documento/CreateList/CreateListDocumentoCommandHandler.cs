using Domain.Entities.Invitados.Documentos;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Documento.CreateList
{
    public class CreateListDocumentoCommandHandler
        : IRequestHandler<CreateListDocumentoCommand, ErrorOr<bool>>
    {
        private readonly IInvitadoDocumentoRepository _repository;
        private readonly ILogger<CreateListDocumentoCommandHandler> _logger;

        public CreateListDocumentoCommandHandler(
            IInvitadoDocumentoRepository repository,
            ILogger<CreateListDocumentoCommandHandler> logger
        )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateListDocumentoCommand command,
            CancellationToken ct
        )
        {
            var entidades = command.Documentos
                .Select(dto => new InvitadoDocumento(
                    invitadoId: dto.InvitadoId,
                    tipoDocCodigo: dto.TipoDocumentoCodigo,
                    docIdentidad: dto.DocumentoIdentidad,
                    esPrioridad: dto.EsPrioridad
                ))
                .ToList();

            var ids = await _repository.AddListAsync(entidades, ct);
            return true;
        }
    }
}
