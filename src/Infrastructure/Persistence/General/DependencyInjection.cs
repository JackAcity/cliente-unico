using Application.Common.Mappings;
using Application.Data.Auditoria;
using Application.Data.General;
using Domain.Primitives;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.General;

public static class DependencyInjection
{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, DatabaseSettings settings)
    {
                services.AddPersistence(settings);
                return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, DatabaseSettings settings)
    {


        switch (settings.Provider)
        {
            case DatabaseProvider.SqlServer:
                AddDbContextSqlServer(services, settings);
                break;
            case DatabaseProvider.PostgreSql:
                AddDbContextPostgreSql(services, settings);
                break;
            case DatabaseProvider.MongoDb:
                AddDbContextMongoDb(services, settings);
                break;
            case DatabaseProvider.Mysql:
                AddDbContextMysqlDb(services, settings);
                break;
            case DatabaseProvider.Oracle:
                AddDbContextOracleDb(services, settings);
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
        services.AddScoped<IApplicationGeneralDbContext>(sp => sp.GetRequiredService<ApplicationDbContextPostgreSql>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContextPostgreSql>());
    }
    private static void AddDbContextSqlServer(IServiceCollection services, DatabaseSettings databaseSettings)
        {
               

        }

        private static void AddDbContextMongoDb(IServiceCollection services, DatabaseSettings databaseSettings)
        {
                
        }

        private static void AddDbContextMysqlDb(IServiceCollection services, DatabaseSettings databaseSettings)
        {
             
        }

        private static void AddDbContextOracleDb(IServiceCollection services, DatabaseSettings   databaseSettings)
        {
               
        }
}