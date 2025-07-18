using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.SQS;
using Amazon.SQS.Model;

// Ensamblado de Lambda para la deserialización del evento SQS.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RegistroCalimaco;

/// <summary>
/// Modelo de datos para el objeto 'user' que llega en el webhook de Calimaco.
/// Se usan 'records' de C# para una definición concisa e inmutable.
/// </summary>
public record CalimacoUser(
    string alias,
    string email,
    string firstname,
    string lastname,
    string currency,
    string country,
    string national_id
);

/// <summary>
/// Modelo para el mensaje que se enviará a la cola de salida.
/// </summary>
public record OutputMessage(
    string Status,
    string Alias,
    string Email,
    string ProcessedTimestamp
);

public class Function
{
    // Cliente de SQS para enviar mensajes. Se inicializa una vez por instancia de Lambda.
    private readonly IAmazonSQS _sqsClient = new AmazonSQSClient();
    private readonly string? _outputQueueUrl;

    public Function()
    {
        // Lee la URL de la cola de salida desde las variables de entorno de la Lambda.
        _outputQueueUrl = Environment.GetEnvironmentVariable("OUTPUT_QUEUE_URL");
        if (string.IsNullOrEmpty(_outputQueueUrl))
        {
            throw new InvalidOperationException("La variable de entorno 'OUTPUT_QUEUE_URL' no está configurada.");
        }
    }

    /// <summary>
    /// Punto de entrada de la función Lambda, se activa con un evento de SQS.
    /// </summary>
    /// <param name="sqsEvent">El evento de SQS que contiene uno o más mensajes.</param>
    /// <param name="context">El contexto de la ejecución de Lambda.</param>
    public async Task FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
    {
        context.Logger.LogInformation($"Procesando {sqsEvent.Records.Count} mensaje(s) del lote.");

        // Procesa cada mensaje recibido en el lote del evento SQS.
        foreach (var record in sqsEvent.Records)
        {
            try
            {
                await ProcessMessageAsync(record, context);
            }
            catch (Exception ex)
            {
                // Si falla el procesamiento de un mensaje, se loguea el error y se relanza la excepción.
                // Esto hará que el mensaje vuelva a la cola (según la configuración de reintentos)
                // o se envíe a una Dead-Letter Queue (DLQ) si está configurada.
                context.Logger.LogError($"Error al procesar el mensaje {record.MessageId}: {ex.Message}");
                throw;
            }
        }
    }

    /// <summary>
    /// Procesa un único mensaje de la cola SQS.
    /// </summary>
    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Procesando mensaje con ID: {message.MessageId}");

        // 1. Deserializa el cuerpo del mensaje, que es el JSON del usuario de Calimaco.
        var userPayload = JsonSerializer.Deserialize<CalimacoUser>(message.Body);

        if (userPayload == null)
        {
            context.Logger.LogWarning($"El cuerpo del mensaje {message.MessageId} está vacío o mal formado.");
            return;
        }

        // --- AQUÍ VA TU LÓGICA DE NEGOCIO ---
        // Puedes validar los datos, interactuar con una base de datos, llamar a otra API, etc.
        // Por ahora, solo logueamos la información.
        context.Logger.LogInformation($"Registro procesado para el alias: {userPayload.alias}, Email: {userPayload.email}");
        // ------------------------------------

        // 2. Crea el mensaje de salida.
        var outputMessage = new OutputMessage(
            Status: "PROCESADO",
            Alias: userPayload.alias,
            Email: userPayload.email,
            ProcessedTimestamp: DateTime.UtcNow.ToString("o") // Formato ISO 8601
        );

        // 3. Serializa el mensaje de salida a JSON.
        var messageBody = JsonSerializer.Serialize(outputMessage);

        // 4. Envía el mensaje a la cola SQS de salida.
        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = _outputQueueUrl,
            MessageBody = messageBody
        };

        await _sqsClient.SendMessageAsync(sendMessageRequest);
        context.Logger.LogInformation($"Mensaje enviado a la cola de salida para el alias: {userPayload.alias}");
    }
}