using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities.Invitados.Notas;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Notas.CreateList
{
    public class CreateListNotaCommandHandler
        : IRequestHandler<CreateListNotaCommand, ErrorOr<bool>>
    {
        private readonly IInvitadoNotaRepository _repository;
        private readonly ILogger<CreateListNotaCommandHandler> _logger;

        public CreateListNotaCommandHandler(
            IInvitadoNotaRepository repository,
            ICurrentUserService currentUser,
            ILogger<CreateListNotaCommandHandler> logger
        )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateListNotaCommand command,
            CancellationToken ct
        )
        {
            var entidades = command.Notas
                .Select(dto => new InvitadoNota(
                    invitadoId: dto.InvitadoId,
                    comentario: dto.Comentario
                ))
                .ToList();

            var ids = await _repository.AddListAsync(entidades, ct);
            return true;
        }
    }
}
