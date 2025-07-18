using Application.Common.Exceptions;
using Application.Invitados.Preferencial;
using Application.Services;
using Domain.Entities.Invitados.InvitadoPreferencias;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invitados.Preferencia
{
    public class CreateListPreferenciaCommandHandler
        : IRequestHandler<CreateListPreferenciaCommand, ErrorOr<bool>>
    {
        private readonly IInvitadoPreferenciaRepository _repository;
        private readonly ILogger<CreateListPreferenciaCommandHandler> _logger;

        public CreateListPreferenciaCommandHandler(
            IInvitadoPreferenciaRepository repository,
            ILogger<CreateListPreferenciaCommandHandler> logger
        )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateListPreferenciaCommand command,
            CancellationToken ct
        )
        {
            var entidades = command.Preferencias
                .Select(dto => new InvitadoPreferencia(
                    categoriaId: dto.CategoriaId,
                    valor: dto.Valor,
                    leGusta: dto.LeGusta,
                    invitadoId: dto.InvitadoId
                ))
                .ToList();

            var ids = await _repository.AddListAsync(entidades, ct);
            return true;
        }
    }
}
