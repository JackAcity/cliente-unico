using Application.Common.Exceptions;
using Application.Invitados.Correos.Create;
using Application.Invitados.Correos.CreateList;
using Application.Invitados.Direccion.Create;
using Application.Invitados.Direccion.CreateList;
using Application.Invitados.Documento.Create;
using Application.Invitados.Documento.CreateList;
using Application.Invitados.InvitadoPincipal.Create;
using Application.Invitados.Invitados.Create;
using Application.Invitados.InvitadosEtiquetas.CreateList;
using Application.Invitados.Notas.Create;
using Application.Invitados.Notas.CreateList;
using Application.Invitados.Preferencial;
using Application.Invitados.Telefono;
using Application.Services;
using AutoMapper;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Invitados.InvitadoOrchestrator.Create;
public class CreateInvitadoOrchestratorCommandHandler : IRequestHandler<CreateInvitadoOrchestratorCommand, ErrorOr<Guid>>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateInvitadoOrchestratorCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateInvitadoOrchestratorCommandHandler(
        IMediator mediator,
        IUnitOfWork unitOfWork,
        ICurrentUserService user,
        IMapper mapper,
        ILogger<CreateInvitadoOrchestratorCommandHandler> logger)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateInvitadoOrchestratorCommand command, CancellationToken ct)
    {
        try
        {
            var invitadoId = await CreateInvitadoAsync(command, ct);

            await AddPrincipalAsync(command.Principal,invitadoId, ct);
            await AddCorreosAsync(command.Correos, invitadoId, ct);
            await AddTelefonosAsync(command.Telefonos,  invitadoId, ct);
            await AddDocumentosAsync(command.Documentos, invitadoId, ct);
            await AddPreferenciaAsync(command.Preferencias, invitadoId, ct);
            await AddDireccionAsync(command.Direcciones, invitadoId, ct);
            await AddEtiquetasAsync(command.EtiquetasIds, invitadoId, ct);
            await AddNotasAsync(command.Notas, invitadoId, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            _logger.LogInformation("Invitado creado correctamente: {InvitadoId}", invitadoId);

            return invitadoId;
        }
        catch (DomainErrorException ie)
        {
            _logger.LogError("Error en orquestador: {Error}", ie.Errors);
            return ie.Errors;
        }
    }

    private async Task<Guid> CreateInvitadoAsync(CreateInvitadoOrchestratorCommand cmd, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateInvitadoCommand(
            cmd.PrimerNombre, cmd.SegundoNombre, cmd.ApellidoPaterno, cmd.ApellidoMaterno,
            cmd.EstadoCivilId, cmd.FechaNacimiento, cmd.GeneroId, cmd.PaisId
        ), ct);
        if (result.IsError) throw new DomainErrorException(result.FirstError);
        return result.Value;
    }

    private async Task AddPrincipalAsync(CreateDatosPrincipalesDto principal, Guid invitadoId, CancellationToken ct)
    {
        var principalCommand = _mapper.Map<CreateDatosPrincipalesCommand>(principal)with{ InvitadoId = invitadoId };
        var result = await _mediator.Send(principalCommand, ct);
        if (result.IsError) throw new DomainErrorException(result.FirstError);
    }

    private async Task AddCorreosAsync(IEnumerable<CreateCorreoDto>? correos,Guid invitadoId, CancellationToken ct)
    {
        var correosCmd = correos
            .Select(dto =>
                _mapper.Map<CreateCorreoCommand>(dto)
                       with
                { InvitadoId = invitadoId }
            )
            .ToList();
        var cmd = new CreateListCorreoCommand(correosCmd);
        var result = await _mediator.Send(cmd, ct);
        if (result.IsError) throw new DomainErrorException(result.FirstError);
    }

    private async Task AddTelefonosAsync(IEnumerable<CreateTelefonoDto>? telefonos, Guid invitadoId, CancellationToken ct)
    {
        var telefonosCommand = telefonos
           .Select(dto =>
               _mapper.Map<CreateTelefonoCommand>(dto)
                      with
               { InvitadoId = invitadoId }
           )
           .ToList();
        var cmd = new CreateListTelefonoCommand(telefonosCommand);
        var result = await _mediator.Send(cmd, ct);
        if (result.IsError) throw new DomainErrorException(result.FirstError);
    }

    private async Task AddDocumentosAsync(IEnumerable<CreateDocumentoDto>? documento, Guid invitadoId, CancellationToken ct)
    {
        var documentoCommand = documento
          .Select(dto =>
              _mapper.Map<CreateDocumentoCommand>(dto)
                     with
              { InvitadoId = invitadoId }
          )
          .ToList();
        var cmd = new CreateListDocumentoCommand(documentoCommand);
        var result = await _mediator.Send(cmd, ct);
        if (result.IsError) throw new DomainErrorException(result.FirstError);
    }

    private async Task AddPreferenciaAsync(IEnumerable<CreatePreferenciaDto>? preferencia, Guid invitadoId, CancellationToken ct)
    {
        var command = preferencia
         .Select(dto =>
             _mapper.Map<CreatePreferenciaCommand>(dto)
                    with
             { InvitadoId = invitadoId }
         )
         .ToList();
        var cmd = new CreateListPreferenciaCommand(command);
        var result = await _mediator.Send(cmd, ct);
        if (result.IsError) throw new DomainErrorException(result.FirstError);
    }

    private async Task AddDireccionAsync(IEnumerable<CreateDireccionDto>? direccion, Guid invitadoId, CancellationToken ct)
    {
        var command = direccion
         .Select(dto =>
             _mapper.Map<CreateDireccionCommand>(dto)
                    with
             { InvitadoId = invitadoId }
         )
         .ToList();
        var cmd = new CreateListDireccionCommand(command);
        var result = await _mediator.Send(cmd, ct);
        if (result.IsError) throw new DomainErrorException(result.FirstError);
    }

    private async Task AddEtiquetasAsync(IEnumerable<long>? etiquetas, Guid invitadoId, CancellationToken ct)
    {
        var command = etiquetas
         .Select(dto =>
             _mapper.Map<CreateInvitadoEtiquetaCommand>(dto)
                    with
             { InvitadoId = invitadoId }
         )
         .ToList();
        var cmd = new CreateListInvitadoEtiquetaCommand(command);
        var result = await _mediator.Send(cmd, ct);
        if (result.IsError) throw new DomainErrorException(result.FirstError);
    }


    private async Task AddNotasAsync(IEnumerable<string>? notas, Guid invitadoId, CancellationToken ct)
    {
        var command = notas
         .Select(dto =>
             _mapper.Map<CreateNotaCommand>(dto)
                    with
             { InvitadoId = invitadoId }
         )
         .ToList();
        var cmd = new CreateListNotaCommand(command);
        var result = await _mediator.Send(cmd, ct);
        if (result.IsError) throw new DomainErrorException(result.FirstError);
    }
}