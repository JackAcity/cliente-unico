using Domain.Entities.Invitados.Correos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Invitados.Configuration.PostgreSql
{
    public class InvitadoCorreoConfiguration : IEntityTypeConfiguration<InvitadoCorreo>
    {
        public void Configure(EntityTypeBuilder<InvitadoCorreo> builder)
        {
            builder.ToTable("tt_invtcor_invitado_correo", "sch_invitado");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id_invitado_correo");
            builder.Property(c => c.InvitadoId)
                .HasColumnName("id_invitado");
            builder.Property(c => c.TipoContactoCodigo)
                .HasColumnName("tcont_v_codigo")
                .HasMaxLength(5)
                .IsRequired();
            builder.Property(c => c.Correo)
                .HasColumnName("invtcor_v_correo")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(c => c.DeseaNotificacion)
                .HasColumnName("invtcor_b_desea_notificacion")
                .IsRequired();
            builder.Property(c => c.EsPrioridad)
                .HasColumnName("invtcor_b_es_prioridad")
                .IsRequired();
            builder.OwnsOne(c => c.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "invtcor"));
        }
    }
}
