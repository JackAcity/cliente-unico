using Application.Common.Exceptions;
using Application.Invitados.Origen.Documento;
using Application.Invitados.Origen.Invitados;
using Application.Invitados.Origen.OrigenAdicionales;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Origen
{
    public class CreateOrigenInvitadoCommandHandler
        : IRequestHandler<CreateOrigenInvitadoCommand, ErrorOr<Guid>>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateOrigenInvitadoCommandHandler> _logger;

        public CreateOrigenInvitadoCommandHandler(
            IMediator mediator,
            IUnitOfWork unitOfWork,
            ILogger<CreateOrigenInvitadoCommandHandler> logger)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ErrorOr<Guid>> Handle(
            CreateOrigenInvitadoCommand command,
            CancellationToken ct)
        {
            try
            {
                var origenId = await CreateOrigenAsync(command, ct);

                await AddCorreosAsync(command.Correos, origenId, ct);
                await AddDocumentosAsync(command.Documentos, origenId, ct);
                await AddTelefonosAsync(command.Telefonos, origenId, ct);
                await AddAdicionalesAsync(command.Adicionales, origenId, ct);

                await _unitOfWork.SaveChangesAsync(ct);
                _logger.LogInformation(
                    "OrigenInvitado creado correctamente: {OrigenId}", origenId);

                return origenId;
            }
            catch (DomainErrorException de)
            {
                _logger.LogError(
                    "Error al crear OrigenInvitado: {Errors}", de.Errors);
                return ErrorOr<Guid>.From(de.Errors);
            }
        }

        private async Task<Guid> CreateOrigenAsync(
            CreateOrigenInvitadoCommand cmd,
            CancellationToken ct)
        {
            var result = await _mediator.Send(new CreateOrigenInvitadoCoreCommand(
                cmd.SistemaCodigo,
                cmd.OrigenId,
                cmd.FechaRegistroOrigen,
                cmd.FechaModificaOrigen,
                cmd.InvitadoId,
                cmd.EstadoId
            ), ct);

            if (result.IsError)
                throw new DomainErrorException(result.Errors);

            return result.Value;
        }

        private async Task AddCorreosAsync(
            IEnumerable<CreateOrigenInvitadoCorreoDto>? correos,
            Guid origenId,
            CancellationToken ct)
        {
            if (correos == null || !correos.Any()) return;

            var cmd = new CreateListOrigenInvitadoCorreoCommand(
                correos.Select(c => new CreateOrigenInvitadoCorreoCoreCommand(
                    c.InvitadoCorreoId,
                    origenId,
                    c.EsValidado,
                    c.CodigoValidacion,
                    c.EsPrincipal
                )).ToList()
            );

            var result = await _mediator.Send(cmd, ct);
            if (result.IsError)
                throw new DomainErrorException(result.Errors);
        }

        private async Task AddDocumentosAsync(
            IEnumerable<CreateOrigenInvitadoDocumentoDto>? documentos,
            Guid origenId,
            CancellationToken ct)
        {
            if (documentos == null || !documentos.Any()) return;

            var cmd = new CreateListOrigenInvitadoDocumentoCommand(
                documentos.Select(d => new CreateOrigenInvitadoDocumentoCoreCommand(
                    d.InvitadoDocumentoId,
                    origenId,
                    d.EsPrincipal
                )).ToList()
            );

            var result = await _mediator.Send(cmd, ct);
            if (result.IsError)
                throw new DomainErrorException(result.Errors);
        }

        private async Task AddTelefonosAsync(
            IEnumerable<CreateOrigenInvitadoTelefonoDto>? telefonos,
            Guid origenId,
            CancellationToken ct)
        {
            if (telefonos == null || !telefonos.Any()) return;

            var cmd = new CreateListOrigenInvitadoTelefonoCommand(
                telefonos.Select(t => new CreateOrigenInvitadoTelefonoCommand(
                    t.InvitadoTelefonoId,
                    origenId,
                    t.EsPrincipal,
                    t.EsValidado,
                    t.CodigoValidacion
                )).ToList()
            );

            var result = await _mediator.Send(cmd, ct);
            if (result.IsError)
                throw new DomainErrorException(result.Errors);
        }

        private async Task AddAdicionalesAsync(
            IEnumerable<CreateOrigenAdicionalDto>? adicionales,
            Guid origenId,
            CancellationToken ct)
        {
            if (adicionales == null || !adicionales.Any()) return;

            var cmd = new CreateListOrigenAdicionalCommand(
                adicionales.Select(a => new CreateOrigenAdicionalCommand(
                    origenId,
                    a.CampoPersonalizadoId,
                    a.Valor
                )).ToList()
            );

            var result = await _mediator.Send(cmd, ct);
            if (result.IsError)
                throw new DomainErrorException(result.Errors);
        }
    }
}
