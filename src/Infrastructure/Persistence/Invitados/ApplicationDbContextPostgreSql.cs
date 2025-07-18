
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
using Infrastructure.Persistence.Invitados.Configuration.PostgreSql;
using Infrastructure.Persistence.Invitados.Configuration.PostgreSql.Invitados;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContextPostgreSql : ApplicationDbContext, IApplicationInvitadosDbContext
{
    public ApplicationDbContextPostgreSql(
        DbContextOptions<ApplicationDbContextPostgreSql> options,
        IPublisher publisher
        )
        : base(options, publisher) { }

    public DbSet<Invitado> Invitados { get; set; }

    public DbSet<InvitadoDatosPrincipales> InvitadoDatosPrincipales { get; set; }
    public DbSet<InvitadoCorreo> InvitadoCorreo { get; set; }
    public DbSet<InvitadoTelefono> InvitadoTelefono { get; set; }
    public DbSet<OrigenInvitadoDocumento> OrigenInvitadoDocumento { get; set; }
    public DbSet<OrigenInvitado> OrigenInvitado { get; set; }
    public DbSet<OrigenAdicional> OrigenAdicional { get; set; }
    public DbSet<OrigenInvitadoCorreo> OrigenInvitadoCorreo { get; set; }
    public DbSet<OrigenInvitadoTelefono> OrigenInvitadoTelefono { get; set; }
    public DbSet<CorreoNotificacion> CorreoNotificacion { get; set; }
    public DbSet<InvitadoDireccion> InvitadoDireccion { get; set; }
    public DbSet<InvitadoDocumento> InvitadoDocumento { get; set; }
    public DbSet<InvitadoEtiqueta> InvitadoEtiqueta { get; set; }
    public DbSet<InvitadoPreferencia> InvitadoPreferencia { get; set; }
    public DbSet<InvitadoNota> InvitadoNota { get; set; }
    public DbSet<TelefonoNotificacion> TelefonoNotificacion { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine("Instanciando ApplicationDbContextPostgreSql");
        modelBuilder.ApplyConfiguration(new EstadoConfiguration());
        modelBuilder.ApplyConfiguration(new EtiquetaConfiguration());
        modelBuilder.ApplyConfiguration(new CategoriaPreferenciaConfiguration());
        modelBuilder.ApplyConfiguration(new CampoPersonalizadoConfiguration());
        modelBuilder.ApplyConfiguration(new InvitadoConfiguracionPostgreSql());
        modelBuilder.ApplyConfiguration(new OrigenInvitadoConfiguration());
        modelBuilder.ApplyConfiguration(new InvitadoDatosPrincipalesConfiguration());
        modelBuilder.ApplyConfiguration(new InvitadoTelefonoConfiguration());
        modelBuilder.ApplyConfiguration(new InvitadoCorreoConfiguration());
        modelBuilder.ApplyConfiguration(new InvitadoDocumentoConfiguration());
        modelBuilder.ApplyConfiguration(new InvitadoPreferenciaConfiguration());
        modelBuilder.ApplyConfiguration(new InvitadoEtiquetaConfiguration());
        modelBuilder.ApplyConfiguration(new NotaConfiguration());
        modelBuilder.ApplyConfiguration(new OrigenAdicionalConfiguration());
        modelBuilder.ApplyConfiguration(new InvitadoDireccionConfiguration());
        modelBuilder.ApplyConfiguration(new OrigenInvitadoTelefonoConfiguration());
        modelBuilder.ApplyConfiguration(new OrigenInvitadoCorreoConfiguration());
        modelBuilder.ApplyConfiguration(new OrigenInvitadoDocumentoConfiguration());
        modelBuilder.ApplyConfiguration(new CorreoNotificacionConfiguration());
        modelBuilder.ApplyConfiguration(new TelefonoNotificacionConfiguration());
    }
}