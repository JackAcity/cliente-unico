using Domain.Entities.Invitados.Telefonos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Invitados.Configuration.PostgreSql
{
    public class InvitadoTelefonoConfiguration : IEntityTypeConfiguration<InvitadoTelefono>
    {
        public void Configure(EntityTypeBuilder<InvitadoTelefono> builder)
        {
            builder.ToTable("tt_invttelf_invitado_telefono", "sch_invitado");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnName("id_invitado_telefono");
            builder.Property(t => t.InvitadoId)
                .HasColumnName("id_invitado");
            builder.Property(t => t.TipoContactoCodigo)
                .HasColumnName("tcont_v_codigo")
                .HasMaxLength(5)
                .IsRequired();
            builder.Property(t => t.PaisPrefijo)
                .HasColumnName("pa_v_prefijo")
                .HasMaxLength(5)
                .IsRequired();
            builder.Property(t => t.Numero)
                .HasColumnName("invttelf_v_numero")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(t => t.DeseaNotificacion)
                .HasColumnName("invttelf_b_desea_notificacion")
                .IsRequired();
            builder.OwnsOne(t => t.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "invttelf"));
        }
    }
}
