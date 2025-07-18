using Application.Common.Mappings;
using Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection("DatabaseSettings").Get<List<DatabaseSettings>>()!;

        foreach (var connection in databaseSettings)
        {
            switch (connection.Database)
            {
                case "bd_invitado":
                    Persistence.Invitados.DependencyInjection.AddInfrastructure(services, connection);
                    break;

                case "GeneralDb":
                    Persistence.General.DependencyInjection.AddInfrastructure(services, connection);
                    break;

                case "AuditoriaDb":
                    Persistence.Auditoria.DependencyInjection.AddInfrastructure(services, connection);
                    break;

                default:
                    throw new InvalidOperationException($"No handler for DB named '{connection.Database}'");
            }
        }

        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }
}
