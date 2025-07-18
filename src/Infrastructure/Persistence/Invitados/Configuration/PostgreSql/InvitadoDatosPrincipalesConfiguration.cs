using Domain.Entities.Invitados.DatosPrincipales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Invitados.Configuration.PostgreSql
{
    public class InvitadoDatosPrincipalesConfiguration : IEntityTypeConfiguration<InvitadoDatosPrincipales>
    {
        public void Configure(EntityTypeBuilder<InvitadoDatosPrincipales> builder)
        {
            builder.ToTable("tt_invtdtp_invitado_datos_principales", "sch_invitado");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .HasColumnName("id_invitado_datos_principales");
            builder.Property(d => d.TipoDocumentoCodigo)
                .HasColumnName("tdoc_v_codigo")
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(d => d.DocumentoIdentidad)
                .HasColumnName("invtdtp_documento_identidad")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(d => d.Correo)
                .HasColumnName("invtdtp_correo")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(d => d.Numero)
                .HasColumnName("invtdtp_numero")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(d => d.Direccion)
                .HasColumnName("invtdtp_direccion")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(d => d.Referencia)
                .HasColumnName("invtdtp_referencia")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(d => d.InvitadoId)
                .HasColumnName("id_invitado");
            builder.OwnsOne(d => d.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "invtdtp"));
        }
    }
}
