using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Domain.ValueObjects;

namespace Infrastructure.Persistence.Invitados.Configuration.PostgreSql;
public static class AuditConfiguration
{

    public static void ConfigureAuditRecord<T>(OwnedNavigationBuilder<T, AuditInfo> builder, string prefix) where T : class
    {
        var dateTimeToUtcConverter = new ValueConverter<DateTime, DateTime>(
            v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        builder.Property(e => e.FechaCreacion)
                 .HasColumnName($"{prefix}_dt_fecha_creacion")
                 .IsRequired()
                 .HasConversion(dateTimeToUtcConverter)
                 .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(e => e.UsuarioCreacion)
            .HasColumnName($"{prefix}_v_usuario_creacion")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.FechaModificacion)
            .HasColumnName($"{prefix}_dt_fecha_modificacion")
            .IsRequired()
            .HasConversion(dateTimeToUtcConverter)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(e => e.UsuarioModificacion)
            .HasColumnName($"{prefix}_v_usuario_modificacion")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.Activo)
            .HasColumnName($"{prefix}_b_activo")
            .IsRequired()
            .HasDefaultValue(true);
    }

}
