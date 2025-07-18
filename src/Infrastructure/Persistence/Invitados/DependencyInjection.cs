using Application.Common.Mappings;
using Application.Data.Invitados;
using Application.Services;
using Domain.Entities.Invitados.CorreoNotificaciones;
using Domain.Entities.Invitados.Correos;
using Domain.Entities.Invitados.DatosPrincipales;
using Domain.Entities.Invitados.Direcciones;
using Domain.Entities.Invitados.Documentos;
using Domain.Entities.Invitados.EtiquetaInvitados;
using Domain.Entities.Invitados.InvitadoPreferencias;
using Domain.Entities.Invitados.Invitados;
using Domain.Entities.Invitados.Notas;
using Domain.Entities.Invitados.Origenes;
using Domain.Entities.Invitados.Telefonos;
using Domain.Primitives;
using Infrastructure.Common;
using Infrastructure.Persistence.Invitados.Repositories.CorreoNotificaciones;
using Infrastructure.Persistence.Invitados.Repositories.Correos;
using Infrastructure.Persistence.Invitados.Repositories.DatosPrincipales;
using Infrastructure.Persistence.Invitados.Repositories.Direcciones;
using Infrastructure.Persistence.Invitados.Repositories.Documentos;
using Infrastructure.Persistence.Invitados.Repositories.Etiquetas;
using Infrastructure.Persistence.Invitados.Repositories.InvitadoPreferencias;
using Infrastructure.Persistence.Invitados.Repositories.Invitados;
using Infrastructure.Persistence.Invitados.Repositories.Notas;
using Infrastructure.Persistence.Invitados.Repositories.Origenes;
using Infrastructure.Persistence.Invitados.Repositories.TelefonoNotificaciones;
using Infrastructure.Persistence.Invitados.Repositories.Telefonos;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Invitados;

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
        services.AddScoped<AuditInterceptor>();
        services.AddDbContext<ApplicationDbContextPostgreSql>((sp,options) =>
                          options.UseNpgsql(databaseSettings.ConnectionString)
                          .AddInterceptors(sp.GetRequiredService<AuditInterceptor>()));
        services.AddScoped<IApplicationInvitadosDbContext>(sp => sp.GetRequiredService<ApplicationDbContextPostgreSql>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContextPostgreSql>());
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IInvitadoRepository, InvitadoRepository>();
        services.AddScoped<IInvitadoDatosPrincipalesRepository, InvitadoDatosPrincipalesRepository>();
        services.AddScoped<IInvitadoCorreoRepository, InvitadoCorreoRepository>();
        services.AddScoped<IInvitadoTelefonoRepository, InvitadoTelefonoRepository>();
        services.AddScoped<IOrigenAdicionalRepository, OrigenAdicionalRepository>();
        services.AddScoped<IOrigenInvitadoCorreoRepository, OrigenInvitadoCorreoRepository>();
        services.AddScoped<IOrigenInvitadoDocumentoRepository, OrigenInvitadoDocumentoRepository>();
        services.AddScoped<IOrigenInvitadoRepository, OrigenInvitadoRepository>();
        services.AddScoped<IOrigenInvitadoTelefonoRepository, OrigenInvitadoTelefonoRepository>();
        services.AddScoped<ICorreoNotificacionRepository, CorreoNotificacionRepository>();
        services.AddScoped<IInvitadoDireccionRepository, InvitadoDireccionRepository>();
        services.AddScoped<IInvitadoDocumentoRepository, InvitadoDocumentoRepository>();
        services.AddScoped<IInvitadoEtiquetaRepository, InvitadoEtiquetasRepository>();
        services.AddScoped<IInvitadoPreferenciaRepository, InvitadoPreferenciasRepository>();
        services.AddScoped<IInvitadoNotaRepository, InvitadoNotasRepository>();
        services.AddScoped<INotificacionTelefonoRepository, TelefonoNotificacionRepository>();
        
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