
using Domain.Entities.Invitados.CampoPersonalizados;
using Domain.Entities.Invitados.CategoriaPreferencias;
using Domain.Entities.Invitados.CorreoNotificaciones;
using Domain.Entities.Invitados.Direcciones;
using Domain.Entities.Invitados.Documentos;
using Domain.Entities.Invitados.Estados;
using Domain.Entities.Invitados.EtiquetaInvitados;
using Domain.Entities.Invitados.Etiquetas;
using Domain.Entities.Invitados.InvitadoPreferencias;
using Domain.Entities.Invitados.Notas;
using Domain.Entities.Invitados.Origenes;
using Domain.Entities.Invitados.Telefonos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Invitados.Configuration.PostgreSql.Invitados
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("tt_est_estado", "sch_invitado");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id_estado");
            builder.Property(e => e.Nombre)
                .HasColumnName("est_v_nombre")
                .HasMaxLength(20)
                .IsRequired();
            builder.OwnsOne(e => e.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "est"));
        }
    }
    public class EtiquetaConfiguration : IEntityTypeConfiguration<Etiqueta>
    {
        public void Configure(EntityTypeBuilder<Etiqueta> builder)
        {
            builder.ToTable("tm_etiq_etiqueta", "sch_invitado");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id_etiqueta");
            builder.Property(e => e.Nombre)
                .HasColumnName("etiq_v_nombre")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(e => e.Descripcion)
                .HasColumnName("etiq_v_descripcion")
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(e => e.EsAutomatico)
                .HasColumnName("etiq_b_es_automatico")
                .IsRequired();
            builder.OwnsOne(e => e.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "etiq"));
        }
    }
    public class CategoriaPreferenciaConfiguration : IEntityTypeConfiguration<CategoriaPreferencia>
    {
        public void Configure(EntityTypeBuilder<CategoriaPreferencia> builder)
        {
            builder.ToTable("tm_catpref_categoria_preferencia", "sch_invitado");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id_categoria_preferencia");
            builder.Property(c => c.Nombre)
                .HasColumnName("catpref_v_nombre")
                .HasMaxLength(50)
                .IsRequired();
            builder.OwnsOne(c => c.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "catpref"));
        }
    }
    public class CampoPersonalizadoConfiguration : IEntityTypeConfiguration<CampoPersonalizado>
    {
        public void Configure(EntityTypeBuilder<CampoPersonalizado> builder)
        {
            builder.ToTable("tt_camp_campo_personalizado", "sch_invitado");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id_campo_personalizado");
            builder.Property(c => c.Nombre)
                .HasColumnName("camp_v_nombre")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(c => c.Visualizar)
                .HasColumnName("camp_b_visualizar")
                .IsRequired();
            builder.OwnsOne(c => c.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "camp"));
        }
    }

    public class OrigenInvitadoConfiguration : IEntityTypeConfiguration<OrigenInvitado>
    {
        public void Configure(EntityTypeBuilder<OrigenInvitado> builder)
        {
            builder.ToTable("tt_orinvt_origen_invitado", "sch_invitado");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasColumnName("id_origen_invitado");
            builder.Property(o => o.SistemaCodigo)
                .HasColumnName("sis_v_codigo")
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(o => o.OrigenId)
                .HasColumnName("orinvt_v_invitado_origen_id")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(o => o.FechaRegistroOrigen)
                .HasColumnName("orinvt_dt_fecha_registro_origen");
            builder.Property(o => o.FechaModificaOrigen)
                .HasColumnName("orinvt_dt_fecha_modifica_origen");
            builder.Property(o => o.InvitadoId)
                .HasColumnName("id_invitado");
            builder.Property(o => o.EstadoId)
                .HasColumnName("id_estado");
            builder.OwnsOne(o => o.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "orinvt"));
        }
    }
    public class InvitadoDocumentoConfiguration : IEntityTypeConfiguration<InvitadoDocumento>
    {
        public void Configure(EntityTypeBuilder<InvitadoDocumento> builder)
        {
            builder.ToTable("tt_invtdoc_invitado_documento", "sch_invitado");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .HasColumnName("id_invitado_documento");
            builder.Property(d => d.InvitadoId)
                .HasColumnName("id_invitado");
            builder.Property(d => d.TipoDocumentoCodigo)
                .HasColumnName("tdoc_v_codigo")
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(d => d.DocumentoIdentidad)
                .HasColumnName("invtdoc_v_documento_identidad")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(d => d.EsPrioridad)
                .HasColumnName("invtdoc_b_es_prioridad")
                .IsRequired();
            builder.OwnsOne(d => d.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "invtdoc"));
        }
    }
    public class InvitadoPreferenciaConfiguration : IEntityTypeConfiguration<InvitadoPreferencia>
    {
        public void Configure(EntityTypeBuilder<InvitadoPreferencia> builder)
        {
            builder.ToTable("tt_invtpref_invitado_preferencia", "sch_invitado");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("id_invitado_preferencia")
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn();
            builder.Property(p => p.InvitadoId)
                .HasColumnName("id_invitado");
            builder.Property(p => p.CategoriaPreferenciaId)
                .HasColumnName("id_categoria_preferencia");
            builder.Property(p => p.Valor)
                .HasColumnName("invtpref_v_valor")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.LeGusta)
                .HasColumnName("invtpref_b_le_gusta")
                .IsRequired();
            builder.OwnsOne(p => p.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "invtpref"));
            builder.HasOne(e => e.Invitado)
             .WithMany(i => i.Preferencias)
             .HasForeignKey(e => e.InvitadoId)
             .HasConstraintName("fk_pref_invitado")
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class InvitadoEtiquetaConfiguration : IEntityTypeConfiguration<InvitadoEtiqueta>
    {
        public void Configure(EntityTypeBuilder<InvitadoEtiqueta> builder)
        {
            builder.ToTable("tt_etinvet_etiqueta_invitado", "sch_invitado");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id_etiqueta_invitado")
                 .ValueGeneratedOnAdd()         
            .UseIdentityAlwaysColumn();
            builder.Property(e => e.EtiquetaId)
                .HasColumnName("id_etiqueta");
            builder.Property(e => e.InvitadoId)
                .HasColumnName("id_invitado");
            builder.OwnsOne(e => e.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "etinvet"));
            builder.HasOne(e => e.Invitado)
               .WithMany(i => i.Etiquetas)
               .HasForeignKey(e => e.InvitadoId)
               .HasConstraintName("fk_etinvet_invitado")
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class NotaConfiguration : IEntityTypeConfiguration<InvitadoNota>
    {
        public void Configure(EntityTypeBuilder<InvitadoNota> builder)
        {
            builder.ToTable("tt_not_nota", "sch_invitado");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id)
                .HasColumnName("id_nota");
            builder.Property(n => n.InvitadoId)
                .HasColumnName("id_invitado");
            builder.Property(n => n.Comentario)
                .HasColumnName("not_v_comentario")
                .HasMaxLength(200)
                .IsRequired();
            builder.OwnsOne(n => n.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "not"));
            builder.HasOne(e => e.Invitado)
               .WithMany(i => i.InvitadoNotas)
               .HasForeignKey(e => e.InvitadoId)
               .HasConstraintName("fk_not_invitado")
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class OrigenAdicionalConfiguration : IEntityTypeConfiguration<OrigenAdicional>
    {
        public void Configure(EntityTypeBuilder<OrigenAdicional> builder)
        {
            builder.ToTable("tt_orad_origen_adicional", "sch_invitado");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasColumnName("id_origen_adicional");
            builder.Property(o => o.OrigenInvitadoId)
                .HasColumnName("id_origen_invitado");
            builder.Property(o => o.CampoPersonalizadoId)
                .HasColumnName("id_campo_personalizado");
            builder.Property(o => o.Valor)
                .HasColumnName("orad_v_valor")
                .HasMaxLength(20)
                .IsRequired();
            builder.OwnsOne(o => o.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "orad"));
        }
    }
    public class InvitadoDireccionConfiguration : IEntityTypeConfiguration<InvitadoDireccion>
    {
        public void Configure(EntityTypeBuilder<InvitadoDireccion> builder)
        {
            builder.ToTable("tt_invtdir_invitado_direccion", "sch_invitado");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .HasColumnName("id_invitado_direccion");
            builder.Property(d => d.InvitadoId)
                .HasColumnName("id_invitado");
            builder.Property(d => d.TipoContactoCodigo)
                .HasColumnName("tcont_v_codigo")
                .HasMaxLength(5)
                .IsRequired();
            builder.Property(d => d.DistritoId)
                .HasColumnName("id_distrito");
            builder.Property(d => d.TipoViaCodigo)
                .HasColumnName("tvurb_v_codigo")
                .HasMaxLength(5)
                .IsRequired();
            builder.Property(d => d.Coordenadas)
                .HasColumnName("invtdir_v_coordenadas")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(d => d.NumeroLote)
                .HasColumnName("invtdir_v_numero_lote")
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(d => d.Piso)
                .HasColumnName("invtdir_i_piso")
                .IsRequired();
            builder.Property(d => d.Departamento)
                .HasColumnName("invtdir_i_departamento")
                .IsRequired();
            builder.Property(d => d.Direccion)
                .HasColumnName("invtdir_v_direccion")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(d => d.Referencia)
                .HasColumnName("invtdir_v_referencia")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(d => d.EsPrincipal)
                .HasColumnName("invtdir_b_es_principal")
                .IsRequired();
            builder.Property(d => d.Prioridad)
                .HasColumnName("invtdir_b_prioridad")
                .IsRequired();
            builder.OwnsOne(d => d.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "invtdir"));
        }
    }
    public class OrigenInvitadoTelefonoConfiguration : IEntityTypeConfiguration<OrigenInvitadoTelefono>
    {
        public void Configure(EntityTypeBuilder<OrigenInvitadoTelefono> builder)
        {
            builder.ToTable("tt_orinvttelf_origen_invitado_telefono", "sch_invitado");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasColumnName("id_origen_invitado_telefono");
            builder.Property(o => o.InvitadoTelefonoId)
                .HasColumnName("id_invitado_telefono");
            builder.Property(o => o.OrigenInvitadoId)
                .HasColumnName("id_origen_invitado");
            builder.Property(o => o.EsPrincipal)
                .HasColumnName("orinvttelf_b_es_principal")
                .IsRequired();
            builder.Property(o => o.EsValidado)
                .HasColumnName("orinvttelf_b_es_validado")
                .IsRequired();
            builder.Property(o => o.CodigoValidacion)
                .HasColumnName("orinvttelf_v_codigo_validacion")
                .HasMaxLength(20)
                .IsRequired();
            builder.OwnsOne(o => o.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "orinvttelf"));
        }
    }
    public class OrigenInvitadoCorreoConfiguration : IEntityTypeConfiguration<OrigenInvitadoCorreo>
    {
        public void Configure(EntityTypeBuilder<OrigenInvitadoCorreo> builder)
        {
            builder.ToTable("tt_orinvtcor_origen_invitado_correo", "sch_invitado");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasColumnName("id_origen_invitado_correo");
            builder.Property(o => o.InvitadoCorreoId)
                .HasColumnName("id_invitado_correo");
            builder.Property(o => o.OrigenInvitadoId)
                .HasColumnName("id_origen_invitado");
            builder.Property(o => o.EsValidado)
                .HasColumnName("orinvtcor_b_es_validado")
                .IsRequired();
            builder.Property(o => o.CodigoValidacion)
                .HasColumnName("orinvtcor_v_codigo_validacion")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(o => o.EsPrincipal)
                .HasColumnName("orinvtcor_b_es_principal")
                .IsRequired();
            builder.OwnsOne(o => o.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "orinvtcor"));
        }
    }
    public class OrigenInvitadoDocumentoConfiguration : IEntityTypeConfiguration<OrigenInvitadoDocumento>
    {
        public void Configure(EntityTypeBuilder<OrigenInvitadoDocumento> builder)
        {
            builder.ToTable("tt_orinvtdoc_origen_invitado_documento", "sch_invitado");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasColumnName("id_origen_invitado_documento");
            builder.Property(o => o.InvitadoDocumentoId)
                .HasColumnName("id_invitado_documento");
            builder.Property(o => o.OrigenInvitadoId)
                .HasColumnName("id_origen_invitado");
            builder.Property(o => o.EsPrincipal)
                .HasColumnName("orinvtdoc_b_es_principal")
                .IsRequired();
            builder.OwnsOne(o => o.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "orinvtdoc"));
        }
    }
    public class CorreoNotificacionConfiguration : IEntityTypeConfiguration<CorreoNotificacion>
    {
        public void Configure(EntityTypeBuilder<CorreoNotificacion> builder)
        {
            builder.ToTable("tt_cnotif_correo_notificacion", "sch_invitado");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id_correo_notificacion");
            builder.Property(c => c.InvitadoCorreoId)
                .HasColumnName("id_invitado_correo");
            builder.Property(c => c.TipoNotificacionCodigo)
                .HasColumnName("tnotif_v_codigo")
                .HasMaxLength(10)
                .IsRequired();
            builder.OwnsOne(c => c.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "cnotif"));
            builder.HasOne(c => c.InvitadoCorreo)  
                .WithMany(i => i.CorreosNotificacion)
                .HasForeignKey(c => c.InvitadoCorreoId)
                .HasConstraintName("fk_cnotif_invitado_correo")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    public class TelefonoNotificacionConfiguration : IEntityTypeConfiguration<TelefonoNotificacion>
    {
        public void Configure(EntityTypeBuilder<TelefonoNotificacion> builder)
        {
            builder.ToTable("tt_tnotif_telefono_notificacion", "sch_invitado");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnName("id_telefono_notificacion");
            builder.Property(t => t.InvitadoTelefonoId)
                .HasColumnName("id_invitado_telefono");
            builder.Property(t => t.MedioNotificacionCodigo)
                .HasColumnName("mnotif_v_codigo")
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(t => t.TipoNotificacionCodigo)
                .HasColumnName("tnotif_v_codigo")
                .HasMaxLength(10)
                .IsRequired();
            builder.OwnsOne(t => t.AuditInfo, a =>
                AuditConfiguration.ConfigureAuditRecord(a, "tnotif"));
        }
    }



}

