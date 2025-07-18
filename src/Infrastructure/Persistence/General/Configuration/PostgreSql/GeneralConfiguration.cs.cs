using Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.General.Configuration.PostgreSql
{
    public class MedioNotificacionConfiguration : IEntityTypeConfiguration<MedioNotificacion>
    {
        public void Configure(EntityTypeBuilder<MedioNotificacion> builder)
        {
            builder.ToTable("tm_mnotif_medio_notificacion");

            builder.HasKey(m => m.Codigo);

            builder.Property(m => m.Codigo)
                .HasColumnName("mnotif_v_codigo")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(m => m.Nombre)
                .HasColumnName("mnotif_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(m => m.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "mnotif"));
        }
    }
    public class TipoNotificacionConfiguration : IEntityTypeConfiguration<TipoNotificacion>
    {
        public void Configure(EntityTypeBuilder<TipoNotificacion> builder)
        {
            builder.ToTable("tm_tnotif_tipo_notificacion");

            builder.HasKey(m => m.Codigo);

            builder.Property(m => m.Codigo)
                .HasColumnName("tnotif_v_codigo")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(m => m.Nombre)
                .HasColumnName("tnotif_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.CodigoPadre)
                .HasColumnName("tnotif_v_codigo_padre")
                .HasMaxLength(10);

            builder.OwnsOne(m => m.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "tnotif"));
        }
    }

    public class MonedaConfiguration : IEntityTypeConfiguration<Moneda>
    {
        public void Configure(EntityTypeBuilder<Moneda> builder)
        {
            builder.ToTable("tm_mond_moneda");

            builder.HasKey(m => m.Codigo);

            builder.Property(m => m.Codigo)
                .HasColumnName("mond_v_codigo")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(m => m.Nombre)
                .HasColumnName("mond_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.Simbolo)
                .HasColumnName("mond_v_simbolo")
                .HasMaxLength(2)
                .IsRequired();

            builder.OwnsOne(m => m.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "mond"));
        }
    }

    public class TipoViaUrbanaConfiguration : IEntityTypeConfiguration<TipoViaUrbana>
    {
        public void Configure(EntityTypeBuilder<TipoViaUrbana> builder)
        {
            builder.ToTable("tm_tvurb_tipo_via_urbana");

            builder.HasKey(m => m.Codigo);

            builder.Property(m => m.Codigo)
                .HasColumnName("tvurb_v_codigo")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(m => m.Nombre)
                .HasColumnName("tvurb_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(m => m.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "tvurb"));
        }
    }

    public class PaisConfiguration : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("tm_pa_pais");

            builder.HasKey(m => m.Codigo);

            builder.Property(m => m.Codigo)
                .HasColumnName("pa_v_codigo")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(m => m.Nombre)
                .HasColumnName("pa_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.Prefijo)
                .HasColumnName("pa_v_prefijo")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(m => m.Icono)
                .HasColumnName("pa_v_icono")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.Nacionalidad)
                .HasColumnName("pa_v_nacionalidad")
                .HasMaxLength(100)
                .IsRequired();

            builder.OwnsOne(m => m.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "pa"));
        }
    }
    public class ProvinciaConfiguration : IEntityTypeConfiguration<Provincia>
    {
        public void Configure(EntityTypeBuilder<Provincia> builder)
        {
            builder.ToTable("tm_prov_provincia");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasColumnName("id_provincia");

            builder.Property(m => m.Nombre)
                .HasColumnName("prov_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.CodigoUbigeo)
                .HasColumnName("prov_v_codigo_ubigeo")
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(m => m.DepartamentoId)
                .HasColumnName("id_departamento");

            builder.OwnsOne(m => m.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "prov"));
        }
    }
    public class DistritoConfiguration : IEntityTypeConfiguration<Distrito>
    {
        public void Configure(EntityTypeBuilder<Distrito> builder)
        {
            builder.ToTable("tm_dist_distrito");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasColumnName("id_distrito");

            builder.Property(d => d.Nombre)
                .HasColumnName("dist_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(d => d.CodigoUbigeo)
                .HasColumnName("dist_v_codigo_ubigeo")
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(d => d.ProvinciaId)
                .HasColumnName("id_provincia");

            builder.OwnsOne(d => d.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "dist"));
        }
    }

    public class TipoContactoConfiguration : IEntityTypeConfiguration<TipoContacto>
    {
        public void Configure(EntityTypeBuilder<TipoContacto> builder)
        {
            builder.ToTable("tm_tcont_tipo_contacto");

            builder.HasKey(t => t.Codigo);

            builder.Property(t => t.Codigo)
                .HasColumnName("tcont_v_codigo")
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(t => t.Nombre)
                .HasColumnName("tcont_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(t => t.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "tcont"));
        }
    }

    public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.ToTable("tm_tdoc_tipo_documento");

            builder.HasKey(t => t.Codigo);

            builder.Property(t => t.Codigo)
                .HasColumnName("tdoc_v_codigo")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(t => t.Nombre)
                .HasColumnName("tdoc_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(t => t.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "tdoc"));
        }
    }

    public class SistemaConfiguration : IEntityTypeConfiguration<Sistema>
    {
        public void Configure(EntityTypeBuilder<Sistema> builder)
        {
            builder.ToTable("tm_sis_sistema");

            builder.HasKey(s => s.Codigo);

            builder.Property(s => s.Codigo)
                .HasColumnName("sis_v_codigo")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(s => s.Nombre)
                .HasColumnName("sis_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.Prioridad)
                .HasColumnName("sis_i_prioridad");

            builder.Property(s => s.SincronizarDatos)
                .HasColumnName("sis_b_sincronizar_datos");

            builder.Property(s => s.TipoProceso)
                .HasColumnName("sis_i_tipo_proceso");

            builder.OwnsOne(s => s.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "sis"));
        }
    }

    public class EstadoCivilConfiguration : IEntityTypeConfiguration<EstadoCivil>
    {
        public void Configure(EntityTypeBuilder<EstadoCivil> builder)
        {
            builder.ToTable("tm_escv_estado_civil");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id_estado_civil");

            builder.Property(e => e.Nombre)
                .HasColumnName("escv_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(e => e.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "escv"));
        }
    }

    public class GeneroConfiguration : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("tm_gen_genero");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasColumnName("id_genero");

            builder.Property(g => g.Nombre)
                .HasColumnName("gen_v_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(g => g.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "gen"));
        }
    }

}
