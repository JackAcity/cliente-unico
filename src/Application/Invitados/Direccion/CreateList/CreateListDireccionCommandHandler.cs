using Application.Services;
using Domain.Entities.Invitados.Direcciones;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.Direccion.CreateList
{
    public class CreateListDireccionCommandHandler
        : IRequestHandler<CreateListDireccionCommand, ErrorOr<bool>>
    {
        private readonly IInvitadoDireccionRepository _repository;
        private readonly ILogger<CreateListDireccionCommandHandler> _logger;

        public CreateListDireccionCommandHandler(
            IInvitadoDireccionRepository repository,
            ILogger<CreateListDireccionCommandHandler> logger
        )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateListDireccionCommand command,
            CancellationToken ct
        )
        {
 
            var entidades = command.Direcciones
                .Select(dto => new InvitadoDireccion(
                    invitadoId: dto.InvitadoId,
                    tipoContCodigo: dto.TipoContactoCodigo,
                    distritoId: dto.DistritoId,
                    tipoViaCodigo: dto.TipoViaCodigo,
                    coordenadas: dto.Coordenadas,
                    numeroLote: dto.NumeroLote,
                    piso: dto.Piso,
                    departamento: dto.Departamento,
                    direccion: dto.Direccion,
                    referencia: dto.Referencia,
                    esPrincipal: dto.EsPrincipal,
                    prioridad: dto.Prioridad
                ))
                .ToList();

            var ids = await _repository.AddListAsync(entidades, ct);
            return true;
        }
    }
}
