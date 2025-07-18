using ErrorOr;
using MediatR;

namespace Application.Invitados.InvitadoOrchestrator.Create;
public record CreateInvitadoOrchestratorCommand(
    string PrimerNombre,
    string SegundoNombre,
    string ApellidoPaterno,
    string ApellidoMaterno,
    long EstadoCivilId,
    string FechaNacimiento,
    long GeneroId,
    string PaisId,
    CreateDatosPrincipalesDto Principal,
    List<CreateTelefonoDto> Telefonos,
    List<CreateCorreoDto> Correos,
    List<CreateDocumentoDto> Documentos,
    List<CreatePreferenciaDto> Preferencias,
    List<long> EtiquetasIds,
    List<string> Notas,
    List<CreateDireccionDto> Direcciones
) : IRequest<ErrorOr<Guid>>;

public record CreateDatosPrincipalesDto(
    string TipoDocumentoCodigo,
    string DocumentoIdentidad,
    string Correo,
    string Numero,
    string Direccion,
    string Referencia
) : IRequest<ErrorOr<Guid>>;

public record CreateTelefonoDto(
    string TipoContactoCodigo,
    string PrefijoPais,
    string Numero,
    bool DeseaNotificacion,
    string MedioNotificacion = "",
    string TipoNotificacion = ""
) : IRequest<ErrorOr<Guid>>;

public record CreateCorreoDto(
    string TipoContactoCodigo,
    string Correo,
    bool DeseaNotificacion,
    bool EsPrioridad,
    string TipoNotificacion = ""
);

public record CreateDocumentoDto(
    string TipoDocumentoCodigo,
    string DocumentoIdentidad,
    bool EsPrioridad
);

public record CreatePreferenciaDto(
    long CategoriaId,
    string Valor,
    bool LeGusta
);

public record CreateDireccionDto(
    string TipoContactoCodigo,
    long? DistritoId,
    string TipoViaCodigo,
    string Coordenadas,
    string NumeroLote,
    int Piso,
    int Departamento,
    string Direccion,
    string Referencia,
    bool EsPrincipal,
    bool Prioridad
);