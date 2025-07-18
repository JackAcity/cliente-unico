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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Application.Data.Invitados;
public interface IApplicationInvitadosDbContext
{
    DbSet<Invitado> Invitados { get; set; }
    DbSet<InvitadoDatosPrincipales> InvitadoDatosPrincipales { get; set; }
    DbSet<CorreoNotificacion> CorreoNotificacion { get; set; }
    DbSet<InvitadoDireccion> InvitadoDireccion { get; set; }
    DbSet<InvitadoDocumento> InvitadoDocumento { get; set; }
    DbSet<InvitadoEtiqueta> InvitadoEtiqueta { get; set; }
    DbSet<InvitadoPreferencia> InvitadoPreferencia { get; set; }
    DbSet<InvitadoNota> InvitadoNota { get; set; }
    DbSet<InvitadoCorreo> InvitadoCorreo { get; set; }
    DbSet<InvitadoTelefono> InvitadoTelefono { get; set; }
    DbSet<OrigenInvitadoDocumento> OrigenInvitadoDocumento { get; set; }
    DbSet<OrigenInvitado> OrigenInvitado { get; set; }
    DbSet<OrigenAdicional> OrigenAdicional { get; set; }
    DbSet<TelefonoNotificacion> TelefonoNotificacion { get; set; }
    DbSet<OrigenInvitadoCorreo> OrigenInvitadoCorreo { get; set; }
    DbSet<OrigenInvitadoTelefono> OrigenInvitadoTelefono { get; set; }
    ChangeTracker ChangeTracker { get; }
    EntityEntry Entry(object entity);
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}