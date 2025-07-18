using Application.Common.Mappings;
using Application.Data.Auditoria;
using Application.Data.Invitados;
using Domain.Primitives;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Auditoria;

public static class DependencyInjection
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, DatabaseSettings settings)
    {
                services.AddPersistence(settings);
                return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, DatabaseSettings settings)
    {

        Console.WriteLine($"Registering DB: {settings.Database} ({settings.Provider})");

        switch (settings.Provider)
        {
            case DatabaseProvider.SqlServer:
                break;
            case DatabaseProvider.PostgreSql:
                AddDbContextPostgreSql(services, settings);
                break;
            case DatabaseProvider.MongoDb:
                break;
            case DatabaseProvider.Mysql:
                break;
            case DatabaseProvider.Oracle:
                break;
            default:
                throw new NotSupportedException($"Unsupported DB provider: {settings.Provider}");
             }
   
                services.AddAutoMapper(typeof(MappingProfile));
                return services;
        }

    private static void AddDbContextPostgreSql(IServiceCollection services, DatabaseSettings databaseSettings)
    {
        services.AddDbContext<ApplicationDbContextPostgreSql>(options =>
                          options.UseNpgsql(databaseSettings.ConnectionString));
        services.AddScoped<IApplicationAuditoriaDbContext>(sp => sp.GetRequiredService<ApplicationDbContextPostgreSql>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContextPostgreSql>());
    }

}