using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities.Invitados.EtiquetaInvitados;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.InvitadosEtiquetas.CreateList
{
public class CreateListInvitadoEtiquetaCommandHandler
    : IRequestHandler<CreateListInvitadoEtiquetaCommand, ErrorOr<bool>>
    {
        private readonly IInvitadoEtiquetaRepository _repository;
        private readonly ILogger<CreateListInvitadoEtiquetaCommandHandler> _logger;

        public CreateListInvitadoEtiquetaCommandHandler(
            IInvitadoEtiquetaRepository repository,
            ILogger<CreateListInvitadoEtiquetaCommandHandler> logger
        )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateListInvitadoEtiquetaCommand command,
            CancellationToken ct
        )
        {
            var entidades = command.Etiquetas
                .Select(dto => new InvitadoEtiqueta(
                    etiquetaId: dto.EtiquetaId,
                    invitadoId: dto.InvitadoId
                ))
                .ToList();

            var ids = await _repository.AddListAsync(entidades, ct);


            return true;
        }
    }
}
