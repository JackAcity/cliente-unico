using Amazon.Lambda.Core;
using System;
using System.Net.Http;
using System.Net.Http.Json; // Usar el nativo de .NET
using System.Threading.Tasks;

// Este serializador es para la entrada y salida de la Lambda (DniInput y LambdaResponse)
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ObtenerDatosReniec
{
    public class Function
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public Function()
        {
            // Leemos la configuración desde las variables de entorno. ¡Más seguro y flexible!
            _apiUrl = Environment.GetEnvironmentVariable("API_PERU_URL")
                      ?? throw new ArgumentNullException("API_PERU_URL no está configurada.");
            _apiKey = Environment.GetEnvironmentVariable("API_KEY_APP")
                      ?? throw new ArgumentNullException("API_KEY_APP no está configurada.");
        }

        public async Task<LambdaResponse> FunctionHandler(DniInput input, ILambdaContext context)
        {
            context.Logger.LogInformation($"Iniciando consulta para DNI: {input.dni}");

            if (string.IsNullOrWhiteSpace(input.dni))
            {
                return new LambdaResponse { Success = false, Message = "El DNI no puede ser nulo o vacío." };
            }

            try
            {
                // Construimos la URL completa
                var requestUrl = $"{_apiUrl}{input.dni}";
                using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                request.Headers.Add("ApiKeyApp", _apiKey);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    // Usamos System.Text.Json, que es nativo y más rápido
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

                    if (apiResponse != null && apiResponse.ok)
                    {
                        var transformedData = TransformarDatos(apiResponse);
                        context.Logger.LogInformation($"Datos procesados exitosamente para DNI: {input.dni}");
                        return new LambdaResponse
                        {
                            Success = true,
                            Message = "Datos de DNI obtenidos y procesados exitosamente.",
                            Data = transformedData
                        };
                    }
                    else
                    {
                        string errorMessage = $"La API externa falló para DNI {input.dni}. Mensaje: {apiResponse?.message ?? "N/A"}";
                        context.Logger.LogError(errorMessage);
                        return new LambdaResponse { Success = false, Message = errorMessage };
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"Error HTTP {(int)response.StatusCode} al llamar a la API para DNI {input.dni}. Contenido: {errorContent}";
                    context.Logger.LogError(errorMessage);
                    return new LambdaResponse { Success = false, Message = errorMessage };
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error inesperado durante el procesamiento para DNI {input.dni}: {ex.Message}";
                context.Logger.LogError(errorMessage);
                return new LambdaResponse { Success = false, Message = errorMessage, Data = ex.StackTrace };
            }
        }

        private object TransformarDatos(ApiResponse data)
        {
            return new
            {
                NumeroDni = data.data.numero,
                NombreCompletoPersona = data.data.nombre_completo,
                Nombres = data.data.nombres,
                ApellidoPaterno = data.data.apellido_paterno,
                ApellidoMaterno = data.data.apellido_materno,
                Direccion = data.data.direccion_completa,
                CodigoVerificacion = data.data.codigo_verificacion
            };
        }
    }

    // --- Clases de datos (sin cambios) ---
    public class DniInput { public string dni { get; set; } }

    public class LambdaResponse { public bool Success { get; set; } public string Message { get; set; } public object Data { get; set; } }

    public class ApiResponse { public Data data { get; set; } public bool ok { get; set; } public string message { get; set; } }

    public class Data { public string direccion { get; set; } public string direccion_completa { get; set; } public string numero { get; set; } public string nombre_completo { get; set; } public string nombres { get; set; } public string apellido_paterno { get; set; } public string apellido_materno { get; set; } public int codigo_verificacion { get; set; } public string[] ubigeo { get; set; } }
}