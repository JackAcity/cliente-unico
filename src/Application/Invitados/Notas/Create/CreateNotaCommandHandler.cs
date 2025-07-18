using Application.Services;
using Domain.Entities.Invitados.Notas;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Notas.Create
{
    public class CreateNotaCommandHandler
        : IRequestHandler<CreateNotaCommand, ErrorOr<long>>
    {
        private readonly IInvitadoNotaRepository _repository;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<CreateNotaCommandHandler> _logger;

        public CreateNotaCommandHandler(
            IInvitadoNotaRepository repository,
            ICurrentUserService currentUser,
            ILogger<CreateNotaCommandHandler> logger
        )
        {
            _repository = repository;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<ErrorOr<long>> Handle(
            CreateNotaCommand command,
            CancellationToken ct
        )
        { 
            var entidad = new InvitadoNota(
                invitadoId: command.InvitadoId,
                comentario: command.Comentario
            );

            var newId = await _repository.AddAsync(entidad, ct);

            return newId;
        }
    }
}
