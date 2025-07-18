using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Application;
using Application.Invitados.InvitadoOrchestrator.Create;
using Infrastructure; 
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CrearInvitadoUnico;

public class Function
{
	private readonly IServiceProvider _serviceProvider;
	private readonly IConfiguration _configuration;

	public Function()
	{
		var services = new ServiceCollection();

		services.AddLogging(config =>
		{
			config.AddConsole();
		});


		var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables();

		_configuration = builder.Build();


		services.AddSingleton<IConfiguration>(_configuration);
		services.AddApplication();
		services.AddInfrastructure(_configuration);

		_serviceProvider = services.BuildServiceProvider();
	}

	public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
	{
		LogToFile("Iniciando Lambda");

		try
		{
			LogToFile($"Request Body: {request.Body}");
			var mediator = _serviceProvider.GetRequiredService<IMediator>();
			var command = JsonConvert.DeserializeObject<CreateInvitadoOrchestratorCommand>(request.Body);

			LogToFile("Deserialización completada."); 

			LogToFile("Enviar mensaje CQRS.");
			var result =await mediator.Send(command!);

			LogToFile("Respuesta de CQRS.");

			var response = new APIGatewayProxyResponse
			{
				StatusCode = 201,
				Body = result.Value.ToString(),
				Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
			}; 

			LogToFile($"Nuevo cliente: {result}");

			return response;
		}
		catch (Exception ex)
		{
			LogToFile($"Error: {ex.Message}");
			LogToFile($"StackTrace: {ex.StackTrace}");

			return new APIGatewayProxyResponse
			{
				StatusCode = 500,
				Body = $"Error interno: {ex.Message}"
			};
		}
	}

	private void LogToFile(string message)
	{
		var logPath = Path.Combine(Directory.GetCurrentDirectory(), "log-lambda.txt");
		File.AppendAllText(logPath, $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
	}
}
