using Application.Services;
using Domain.Entities.Invitados.DatosPrincipales;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Application.Invitados.InvitadoPincipal.Create;
public class CreateDatosPrincipalesCommandHandler : IRequestHandler<CreateDatosPrincipalesCommand, ErrorOr<Guid>>
{
    private readonly ILogger<CreateDatosPrincipalesCommandHandler> _logger;
    private readonly IInvitadoDatosPrincipalesRepository _datosPrincipalesDatosPrincipalesRepository;

    public CreateDatosPrincipalesCommandHandler(
        IInvitadoDatosPrincipalesRepository datosPrincipalesDatosPrincipalesRepository,
        ILogger<CreateDatosPrincipalesCommandHandler> logger)
    {
        _datosPrincipalesDatosPrincipalesRepository = datosPrincipalesDatosPrincipalesRepository;
        _logger = logger;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateDatosPrincipalesCommand command, CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Identifier", string.Concat("Create DatosPrincipales: " + command.DocumentoIdentidad)))
        {
            try
            {
                var datosPrincipales = new InvitadoDatosPrincipales(
                    command.TipoDocumentoCodigo, 
                    command.DocumentoIdentidad,
                    command.Correo, 
                    command.Numero, 
                    command.Direccion,
                    command.Referencia,
                    command.InvitadoId);

                await _datosPrincipalesDatosPrincipalesRepository.AddAsync(datosPrincipales);
                return datosPrincipales.Id;
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error creating DatosPrincipales: {ex.Message}";
                _logger.LogError(errorMessage);

                return Error.Failure(
                    code: "DatosPrincipales.Create.Failure",
                    description: errorMessage
                );
            }
        }
    }
}
