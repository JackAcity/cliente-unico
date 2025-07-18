using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// Atributo a nivel de ensamblado para configurar el serializador de Lambda.
// Esto asegura que AWS Lambda sepa cómo manejar los objetos de entrada y salida.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ObtenerInvitados;

// =================================================================================
// CAPA DE PRESENTACIÓN (LAMBDA)
// Responsable de recibir la petición, orquestar la lógica y devolver una respuesta HTTP.
// =================================================================================

/// <summary>
/// La clase principal de la función Lambda. Actúa como el punto de entrada.
/// </summary>
public class Function
{
    private readonly IGuestService _guestService;

    /// <summary>
    /// Constructor que recibe las dependencias a través de inyección.
    /// Ideal para pruebas unitarias.
    /// </summary>
    public Function(IGuestService guestService)
    {
        _guestService = guestService;
    }

    /// <summary>
    /// Constructor sin parámetros que AWS Lambda utiliza para instanciar la función.
    /// Configura y utiliza el contenedor de inyección de dependencias definido en la clase Startup.
    /// </summary>
    public Function() : this(Startup.ConfigureServices().GetRequiredService<IGuestService>())
    {
    }

    /// <summary>
    /// El manejador principal de la función Lambda que procesa las solicitudes de API Gateway.
    /// </summary>
    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        try
        {
            // 1. Deserializar la entrada. Si falla, el bloque catch lo manejará.
            var paginationRequest = JsonSerializer.Deserialize<GuestPaginationRequestDto>(request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new GuestPaginationRequestDto();

            // Corrección de valores de paginación para evitar errores.
            if (paginationRequest.PageNumber <= 0) paginationRequest.PageNumber = 1;
            if (paginationRequest.PageSize <= 0) paginationRequest.PageSize = 10;

            context.Logger.LogInformation($"Recibida solicitud de paginación: Page={paginationRequest.PageNumber}, Size={paginationRequest.PageSize}, Query='{paginationRequest.SearchQuery}'");

            // 2. Delegar toda la lógica de negocio al servicio.
            var result = await _guestService.GetGuestsAsync(paginationRequest);

            // 3. Construir la respuesta exitosa según el contrato.
            var apiResponse = new ApiResponse<PaginatedResponseDto<GuestDto>>(
                true,
                "Invitados recuperados exitosamente (datos mockeados).",
                result);

            return CreateHttpResponse(200, apiResponse);
        }
        catch (JsonException jsonEx)
        {
            context.Logger.LogError(jsonEx, "Error de formato JSON en la solicitud.");
            var errorResponse = new ApiResponse<object>(false, "Error en el formato JSON de la solicitud.", null, new Dictionary<string, string> { { "JsonFormat", jsonEx.Message } });
            return CreateHttpResponse(400, errorResponse);
        }
        catch (Exception ex)
        {
            context.Logger.LogError(ex, "Error interno inesperado del servidor.");
            var errorResponse = new ApiResponse<object>(false, $"Error interno del servidor al recuperar invitados: {ex.Message}", null, new Dictionary<string, string> { { "InternalError", ex.StackTrace ?? "" } });
            return CreateHttpResponse(500, errorResponse);
        }
    }

    /// <summary>
    /// Método de utilidad para crear una respuesta HTTP estándar.
    /// </summary>
    private APIGatewayProxyResponse CreateHttpResponse(int statusCode, object body)
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = statusCode,
            Body = JsonSerializer.Serialize(body),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }
}

/// <summary>
/// Clase de utilidad para configurar la Inyección de Dependencias.
/// Mantiene la clase Function limpia y enfocada en su responsabilidad principal.
/// </summary>
public static class Startup
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Configura el sistema de logging estándar de .NET para que use el logger de Lambda.
        services.AddLogging(builder => builder.AddLambdaLogger());

        // Registra el servicio de invitados.
        // Usamos AddSingleton porque nuestro servicio mock no tiene estado.
        // En un caso real con Entity Framework, registrarías el DbContext (AddDbContext)
        // y el servicio (AddScoped<IGuestService, GuestService>).
        services.AddSingleton<IGuestService, MockGuestService>();

        return services.BuildServiceProvider();
    }
}


// =================================================================================
// CAPA DE INFRAESTRUCTURA (EL "CÓMO")
// Responsable de la implementación de la lógica de acceso a datos.
// =================================================================================

/// <summary>
/// Implementación de IGuestService que devuelve datos mockeados.
/// En un caso real, aquí iría la lógica de consulta a la base de datos con Entity Framework Core.
/// </summary>
public class MockGuestService : IGuestService
{
    private readonly List<GuestDto> _allGuests;

    public MockGuestService()
    {
        _allGuests = GenerateMockGuests(150);
    }

    public Task<PaginatedResponseDto<GuestDto>> GetGuestsAsync(GuestPaginationRequestDto request)
    {
        // 1. Aplicar filtros
        var filteredGuests = _allGuests.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchQuery))
        {
            var query = request.SearchQuery.ToLowerInvariant();
            filteredGuests = filteredGuests.Where(g =>
                g.PrimerNombre.ToLowerInvariant().Contains(query) ||
                g.ApellidoPaterno.ToLowerInvariant().Contains(query) ||
                g.DocumentoIdentidad.Contains(query));
        }

        if (!string.IsNullOrEmpty(request.Status))
        {
            filteredGuests = filteredGuests.Where(g => g.EstadoDeRiesgo.Equals(request.Status, StringComparison.OrdinalIgnoreCase));
        }

        var totalItems = filteredGuests.Count();

        // 2. Aplicar ordenamiento
        // Usamos un switch para un ordenamiento seguro y eficiente, evitando reflection.
        var orderedGuests = (request.SortBy?.ToLowerInvariant(), request.SortOrder?.ToLowerInvariant()) switch
        {
            ("nombre", "desc") => filteredGuests.OrderByDescending(g => g.PrimerNombre),
            ("apellido", "asc") => filteredGuests.OrderBy(g => g.ApellidoPaterno),
            ("apellido", "desc") => filteredGuests.OrderByDescending(g => g.ApellidoPaterno),
            ("documento", "asc") => filteredGuests.OrderBy(g => g.DocumentoIdentidad),
            ("documento", "desc") => filteredGuests.OrderByDescending(g => g.DocumentoIdentidad),
            ("estadoriesgo", "asc") => filteredGuests.OrderBy(g => g.EstadoDeRiesgo),
            ("estadoriesgo", "desc") => filteredGuests.OrderByDescending(g => g.EstadoDeRiesgo),
            ("id", "asc") => filteredGuests.OrderBy(g => g.Id),
            ("id", "desc") => filteredGuests.OrderByDescending(g => g.Id),
            _ => filteredGuests.OrderBy(g => g.PrimerNombre) // Orden por defecto
        };

        // 3. Aplicar paginación
        var pagedGuests = orderedGuests
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

        var response = new PaginatedResponseDto<GuestDto>
        {
            Guests = pagedGuests,
            Pagination = new PaginationMetadata
            {
                TotalItems = totalItems,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                HasNextPage = request.PageNumber < totalPages,
                HasPreviousPage = request.PageNumber > 1
            }
        };

        return Task.FromResult(response);
    }

    // --- Métodos de utilidad para generar datos de prueba ---
    private List<GuestDto> GenerateMockGuests(int count) => Enumerable.Range(1, count).Select(i => CreateGuest(i)).ToList();
    private GuestDto CreateGuest(int i) => new()
    {
        Id = Guid.NewGuid().ToString(),
        Tarjeta = $"ATL-{123456 + i}",
        Alias = GetRandomAlias(i),
        PrimerNombre = GetRandomFirstName(i),
        SegundoNombre = i % 3 == 0 ? "Alberto" : null,
        ApellidoPaterno = GetRandomLastName(i),
        ApellidoMaterno = "López",
        NombreCompleto = $"{GetRandomFirstName(i)} {(i % 3 == 0 ? "Alberto " : "")}{GetRandomLastName(i)} López",
        DocumentoIdentidad = (70000000 + i).ToString(),
        TipoDocumento = "DNI",
        EstadoUnificadoCliente = GetRandomStatus(i),
        CorreoPrincipal = $"user{i}@example.com",
        TelefonoPrincipal = (987654321 + i).ToString(),
        EstadoDeRiesgo = GetRandomStatus(i),
        Contrato = "Activo",
        ReniecStatus = "Validado",
        Genero = "Masculino",
        Edad = 25 + (i % 30),
        Direccion = $"Av. Siempre Viva 123, Piso {i}, Miraflores"
    };
    private string GetRandomStatus(int i) => (i % 4) switch { 0 => "Sin riesgo", 1 => "Observado", 2 => "Prohibido", _ => "Ludópata" };
    private string GetRandomAlias(int i) => (i % 5) switch { 0 => "JUAPE", 1 => "MARRO", 2 => "LUISA", 3 => "ANATO", _ => "CARLO" } + i;
    private string GetRandomFirstName(int i) => (i % 5) switch { 0 => "Juan", 1 => "María", 2 => "Luis", 3 => "Ana", _ => "Carlos" };
    private string GetRandomLastName(int i) => (i % 5) switch { 0 => "Pérez", 1 => "Rodríguez", 2 => "Sánchez", 3 => "Torres", _ => "Flores" };
}


// =================================================================================
// CAPA DE DOMINIO (EL "QUÉ")
// Responsable de definir los contratos (interfaces) y los objetos de negocio (DTOs).
// =================================================================================

/// <summary>
/// Contrato que define la funcionalidad para obtener invitados.
/// </summary>
public interface IGuestService
{
    Task<PaginatedResponseDto<GuestDto>> GetGuestsAsync(GuestPaginationRequestDto request);
}

// --- DTOs (Data Transfer Objects) ---
// Clases simples que solo sirven para transportar datos entre capas.

/// <summary>
/// Representa los parámetros de entrada para la paginación.
/// </summary>
public class GuestPaginationRequestDto
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public string? SearchQuery { get; set; }
    public string? Status { get; set; }
}

/// <summary>
/// Representa la información de un único invitado en la respuesta.
/// </summary>
public class GuestDto
{
    public string Id { get; set; } = string.Empty;
    public string Tarjeta { get; set; } = string.Empty;
    public string Alias { get; set; } = string.Empty;
    public string PrimerNombre { get; set; } = string.Empty;
    public string? SegundoNombre { get; set; }
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string TipoDocumento { get; set; } = string.Empty;
    public string DocumentoIdentidad { get; set; } = string.Empty;
    public string EstadoUnificadoCliente { get; set; } = string.Empty;
    public string CorreoPrincipal { get; set; } = string.Empty;
    public string TelefonoPrincipal { get; set; } = string.Empty;
    public string EstadoDeRiesgo { get; set; } = string.Empty;
    public string Contrato { get; set; } = string.Empty;
    public string ReniecStatus { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public int? Edad { get; set; }
    public string Direccion { get; set; } = string.Empty;
}

/// <summary>
/// Contiene los metadatos de la paginación.
/// </summary>
public class PaginationMetadata
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}

/// <summary>
/// La estructura de la respuesta exitosa que contiene una lista de items y la paginación.
/// </summary>
public class PaginatedResponseDto<T>
{
    public IEnumerable<T> Guests { get; set; } = Enumerable.Empty<T>();
    public PaginationMetadata Pagination { get; set; } = new();
}

/// <summary>
/// Clase genérica para estandarizar todas las respuestas de la API,
/// cumpliendo con el contrato especificado.
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public Dictionary<string, string>? Errors { get; set; }

    public ApiResponse(bool success, string message, T? data, Dictionary<string, string>? errors = null)
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors;
    }
}