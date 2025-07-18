using Domain.Entities.Invitados.Invitados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Invitados.Configuration.PostgreSql
{
    public class InvitadoConfiguracionPostgreSql : IEntityTypeConfiguration<Invitado>
    {
        public void Configure(EntityTypeBuilder<Invitado> builder)
        {
            builder.ToTable("tt_invt_invitado", "sch_invitado");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("id_invitado");
            builder.Property(i => i.PrimerNombre)
                .HasColumnName("invt_v_primer_nombre")
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(i => i.SegundoNombre)
                .HasColumnName("invt_v_segundo_nombre")
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(i => i.ApellidoPaterno)
                .HasColumnName("invt_v_apellido_paterno")
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(i => i.ApellidoMaterno)
                .HasColumnName("invt_v_apellido_materno")
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(i => i.EstadoCivilId)
                .HasColumnName("id_estado_civil");
            builder.Property(i => i.FechaNacimiento)
                .HasColumnName("invt_dt_fecha_nacimiento");
            builder.Property(i => i.GeneroId)
                .HasColumnName("id_genero");
            builder.Property(i => i.PaisCodigo)
                .HasColumnName("id_pais")
                .HasMaxLength(10);
            builder.OwnsOne(i => i.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "invt"));
            builder.HasMany(i => i.Etiquetas)
                 .WithOne(e => e.Invitado)
                 .HasForeignKey(e => e.InvitadoId)
                 .HasConstraintName("fk_etinvet_invitado")
                 .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(i => i.Preferencias)
                .WithOne(e => e.Invitado)
                .HasForeignKey(e => e.InvitadoId)
                .HasConstraintName("fk_pref_invitado")
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(i => i.InvitadoNotas)
                .WithOne(e => e.Invitado)
                .HasForeignKey(e => e.InvitadoId)
                .HasConstraintName("fk_not_invitado")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
