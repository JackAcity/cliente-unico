using Application.Services;
using Domain.Entities.Invitados.Invitados;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Application.Invitados.Invitados.Create
{
    public class CreateInvitadoCommandHandler : IRequestHandler<CreateInvitadoCommand, ErrorOr<Guid>>
    {
        private readonly ILogger<CreateInvitadoCommandHandler> _logger;
        private readonly IInvitadoRepository _invitadoRepository;

        public CreateInvitadoCommandHandler(
            IInvitadoRepository inventarioRepository,
            ILogger<CreateInvitadoCommandHandler> logger)
        {
            _invitadoRepository = inventarioRepository;
            _logger = logger;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateInvitadoCommand command, CancellationToken cancellationToken) { 
       

                string fechaTextoNacimiento = command.FechaNacimiento;
                if (!DateTime.TryParse(command.FechaNacimiento, out DateTime fechaNacimiento))
                {
                    _logger.LogWarning("Fecha de nacimiento inválida: {Fecha}", command.FechaNacimiento);
                    return Error.Validation("Invitado.FechaNacimiento", "La fecha de nacimiento es inválida.");
                }
                fechaNacimiento = DateTime.SpecifyKind(fechaNacimiento, DateTimeKind.Utc);

                var invitado = new Invitado(
                    command.PrimerNombre,
                    command.SegundoNombre, 
                    command.ApellidoPaterno, 
                    command.ApellidoMaterno, 
                    command.EstadoCivilId,
                    fechaNacimiento,
                    command.GeneroId,
                    command.PaisId);

                var invitadoId =await _invitadoRepository.AddAsync(invitado);
                return invitadoId;
        }
    }

}
