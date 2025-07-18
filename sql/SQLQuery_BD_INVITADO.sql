-- 1) Eliminación y creación de la base de datos
DROP DATABASE IF EXISTS bd_invitado;
CREATE DATABASE bd_invitado;

-- 2) Conexión en psql
USE bd_invitado;

-- 4) Tablas de catálogo propias de invitado

-- 4.1) Estados
CREATE TABLE tt_est_estado (
    id_estado                   BIGINT      NOT NULL PRIMARY KEY,
    est_v_nombre                VARCHAR(20) NOT NULL DEFAULT '',
    est_dt_fecha_creacion       TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    est_v_usuario_creacion      VARCHAR(20) NOT NULL,
    est_dt_fecha_modificacion   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    est_v_usuario_modificacion  VARCHAR(20) NOT NULL,
    est_b_activo                BOOLEAN     NOT NULL DEFAULT TRUE
);

-- 4.2) Etiquetas
CREATE TABLE tm_etiq_etiqueta (
    id_etiqueta                 BIGINT      NOT NULL PRIMARY KEY,
    etiq_v_nombre               VARCHAR(20) NOT NULL DEFAULT '',
    etiq_v_descripcion          VARCHAR(200) NOT NULL DEFAULT '',
    etiq_b_es_automatico        BOOLEAN     NOT NULL DEFAULT FALSE,
    etiq_dt_fecha_creacion      TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    etiq_v_usuario_creacion     VARCHAR(20) NOT NULL,
    etiq_dt_fecha_modificacion  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    etiq_v_usuario_modificacion VARCHAR(20) NOT NULL,
    etiq_b_activo               BOOLEAN     NOT NULL DEFAULT TRUE
);

-- 4.3) Categorías de preferencia
CREATE TABLE tm_catpref_categoria_preferencia (
    id_categoria_preferencia    BIGINT      NOT NULL PRIMARY KEY,
    catpref_v_nombre            VARCHAR(50) NOT NULL DEFAULT '',
    catpref_dt_fecha_creacion   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    catpref_v_usuario_creacion  VARCHAR(20) NOT NULL,
    catpref_dt_fecha_modificacion TIMESTAMP  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    catpref_v_usuario_modificacion VARCHAR(20) NOT NULL,
    catpref_b_activo            BOOLEAN     NOT NULL DEFAULT TRUE
);

-- 4.4) Campos personalizados
CREATE TABLE tt_camp_campo_personalizado (
    id_campo_personalizado      BIGINT      NOT NULL PRIMARY KEY,
    camp_v_nombre               VARCHAR(20) NOT NULL DEFAULT '',
    camp_b_visualizar           BOOLEAN     NOT NULL DEFAULT FALSE,
    camp_dt_fecha_creacion      TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    camp_v_usuario_creacion     VARCHAR(20) NOT NULL,
    camp_dt_fecha_modificacion  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    camp_v_usuario_modificacion VARCHAR(20) NOT NULL,
    camp_b_activo               BOOLEAN     NOT NULL DEFAULT TRUE
);

-- 5) Tablas principales de invitado

-- 5.1) Invitado
CREATE TABLE tt_invt_invitado (
    id_invitado                 UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    invt_v_primer_nombre        VARCHAR(30) NOT NULL DEFAULT '',
    invt_v_segundo_nombre       VARCHAR(30) NOT NULL DEFAULT '',
    invt_v_apellido_paterno     VARCHAR(30) NOT NULL DEFAULT '',
    invt_v_apellido_materno     VARCHAR(30) NOT NULL DEFAULT '',
    id_estado_civil             BIGINT,    -- FK a tm_escv_estado_civil(id_estado_civil)
    invt_dt_fecha_nacimiento    DATE,
    id_genero                   BIGINT,    -- FK a tm_gen_genero(id_genero)
    id_pais                     VARCHAR(10), -- FK a tm_pa_pais(pa_v_codigo)
    invt_dt_fecha_creacion      TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invt_v_usuario_creacion     VARCHAR(20) NOT NULL,
    invt_dt_fecha_modificacion  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invt_v_usuario_modificacion VARCHAR(20) NOT NULL,
    invt_b_activo               BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_invt_pais           FOREIGN KEY (id_pais)          REFERENCES tm_pa_pais(pa_v_codigo)
);

-- 5.2) Origen del invitado
CREATE TABLE tt_orinvt_origen_invitado (
    id_origen_invitado          UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    sis_v_codigo                VARCHAR(10) NOT NULL,  -- FK a tm_sis_sistema(sis_v_codigo)
    orinvt_v_invitado_origen_id VARCHAR(20) NOT NULL DEFAULT '',
    orinvt_dt_fecha_registro_origen TIMESTAMP,
    orinvt_dt_fecha_modifica_origen TIMESTAMP,
    id_invitado                 UUID        NOT NULL,
    id_estado                   BIGINT,   -- FK a tt_est_estado(id_estado)
    orinvt_dt_fecha_creacion    TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orinvt_v_usuario_creacion   VARCHAR(20) NOT NULL,
    orinvt_dt_fecha_modificacion TIMESTAMP  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orinvt_v_usuario_modificacion VARCHAR(20) NOT NULL,
    orinvt_b_activo             BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT fk_orinvt_invitado    FOREIGN KEY (id_invitado) REFERENCES tt_invt_invitado(id_invitado),
    CONSTRAINT fk_orinvt_estado      FOREIGN KEY (id_estado)    REFERENCES tt_est_estado(id_estado)
);

-- 5.3) Datos principales
CREATE TABLE tt_invtdtp_invitado_datos_principales (
    id_invitado_datos_principales UUID     NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    tdoc_v_codigo               VARCHAR(10) NOT NULL, -- FK a tm_tdoc_tipo_documento(tdoc_v_codigo)
    invtdtp_documento_identidad VARCHAR(20) NOT NULL DEFAULT '',
    invtdtp_correo              VARCHAR(50) NOT NULL DEFAULT '',
    invtdtp_numero              VARCHAR(20) NOT NULL DEFAULT '',
    invtdtp_direccion           VARCHAR(100) NOT NULL DEFAULT '',
    invtdtp_referencia          VARCHAR(100) NOT NULL DEFAULT '',
    id_invitado                 UUID,
    invtdtp_dt_fecha_creacion   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtdtp_v_usuario_creacion  VARCHAR(20) NOT NULL,
    invtdtp_dt_fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtdtp_v_usuario_modificacion VARCHAR(20) NOT NULL,
    invtdtp_b_activo            BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_invtdtp_invitado FOREIGN KEY (id_invitado) REFERENCES tt_invt_invitado(id_invitado),
    CONSTRAINT fk_invtdtp_tipo_doc FOREIGN KEY (tdoc_v_codigo) REFERENCES tm_tdoc_tipo_documento(tdoc_v_codigo)
);

-- 5.4) Teléfonos
CREATE TABLE tt_invttelf_invitado_telefono (
    id_invitado_telefono        UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_invitado                 UUID        NOT NULL,
    tcont_v_codigo              VARCHAR(5)  NOT NULL,  -- FK a tm_tcont_tipo_contacto(tcont_v_codigo)
    pa_v_prefijo                VARCHAR(5)  NOT NULL DEFAULT '', -- FK a tm_pa_pais(pa_v_prefijo)
    invttelf_v_numero           VARCHAR(20) NOT NULL DEFAULT '',
    invttelf_b_desea_notificacion BOOLEAN    NOT NULL DEFAULT FALSE,
    invttelf_dt_fecha_creacion  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invttelf_v_usuario_creacion VARCHAR(20) NOT NULL,
    invttelf_dt_fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invttelf_v_usuario_modificacion VARCHAR(20) NOT NULL,
    invttelf_b_activo           BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_tel_invitado   FOREIGN KEY (id_invitado)    REFERENCES tt_invt_invitado(id_invitado),
    CONSTRAINT fk_tel_tipo_cont  FOREIGN KEY (tcont_v_codigo) REFERENCES tm_tcont_tipo_contacto(tcont_v_codigo),
    CONSTRAINT fk_tel_prefijo    FOREIGN KEY (pa_v_prefijo)     REFERENCES tm_pa_pais(pa_v_prefijo)
);

-- 5.5) Correos
CREATE TABLE tt_invtcor_invitado_correo (
    id_invitado_correo          UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_invitado                 UUID        NOT NULL,
    tcont_v_codigo              VARCHAR(5)  NOT NULL,
    invtcor_v_correo            VARCHAR(50) NOT NULL DEFAULT '',
    invtcor_b_desea_notificacion BOOLEAN     NOT NULL DEFAULT FALSE,
    invtcor_b_es_prioridad      BOOLEAN     NOT NULL DEFAULT FALSE,
    invtcor_dt_fecha_creacion   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtcor_v_usuario_creacion  VARCHAR(20) NOT NULL,
    invtcor_dt_fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtcor_v_usuario_modificacion VARCHAR(20) NOT NULL,
    invtcor_b_activo            BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_cor_invitado   FOREIGN KEY (id_invitado)    REFERENCES tt_invt_invitado(id_invitado),
    CONSTRAINT fk_cor_tipo_cont  FOREIGN KEY (tcont_v_codigo) REFERENCES tm_tcont_tipo_contacto(tcont_v_codigo)
);

-- 5.6) Documentos
CREATE TABLE tt_invtdoc_invitado_documento (
    id_invitado_documento       UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_invitado                 UUID        NOT NULL,
    tdoc_v_codigo               VARCHAR(10) NOT NULL,
    invtdoc_v_documento_identidad VARCHAR(20) NOT NULL DEFAULT '',
    invtdoc_b_es_prioridad      BOOLEAN     NOT NULL DEFAULT FALSE,
    invtdoc_dt_fecha_creacion   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtdoc_v_usuario_creacion  VARCHAR(20) NOT NULL,
    invtdoc_dt_fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtdoc_v_usuario_modificacion VARCHAR(20) NOT NULL,
    invtdoc_b_activo            BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_doc_invitado   FOREIGN KEY (id_invitado)    REFERENCES tt_invt_invitado(id_invitado),
    CONSTRAINT fk_doc_tipo_doc   FOREIGN KEY (tdoc_v_codigo)  REFERENCES tm_tdoc_tipo_documento(tdoc_v_codigo)
);

-- 5.7) Preferencias
CREATE TABLE tt_invtpref_invitado_preferencia (
    id_invitado_preferencia     BIGINT      NOT NULL PRIMARY KEY,
    id_invitado                 UUID        NOT NULL,
    id_categoria_preferencia    BIGINT      NOT NULL,
    invtpref_v_valor            VARCHAR(100) NOT NULL DEFAULT '',
    invtpref_b_le_gusta         BOOLEAN     NOT NULL DEFAULT TRUE,
    invtpref_dt_fecha_creacion  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtpref_v_usuario_creacion VARCHAR(20) NOT NULL,
    invtpref_dt_fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtpref_v_usuario_modificacion VARCHAR(20) NOT NULL,
    invtpref_b_activo           BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_pref_invitado  FOREIGN KEY (id_invitado) REFERENCES tt_invt_invitado(id_invitado),
    CONSTRAINT fk_pref_categoria FOREIGN KEY (id_categoria_preferencia) REFERENCES tm_catpref_categoria_preferencia(id_categoria_preferencia)
);

-- 5.8) Etiquetas de invitado
CREATE TABLE tt_etinvet_etiqueta_invitado (
    id_etiqueta_invitado        BIGINT      NOT NULL PRIMARY KEY,
    id_etiqueta                 BIGINT      NOT NULL,
    id_invitado                 UUID        NOT NULL,
    etinvet_dt_fecha_creacion   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    etinvet_v_usuario_creacion  VARCHAR(20) NOT NULL,
    etinvet_dt_fecha_modificacion TIMESTAMP  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    etinvet_v_usuario_modificacion VARCHAR(20) NOT NULL,
    etinvet_b_activo            BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_etinvet_etiqueta FOREIGN KEY (id_etiqueta) REFERENCES tm_etiq_etiqueta(id_etiqueta),
    CONSTRAINT fk_etinvet_invitado FOREIGN KEY (id_invitado) REFERENCES tt_invt_invitado(id_invitado)
);

-- 5.9) Notas
CREATE TABLE tt_not_nota (
    id_nota                     BIGINT      NOT NULL PRIMARY KEY,
    id_invitado                 UUID        NOT NULL,
    not_v_comentario            VARCHAR(200) NOT NULL DEFAULT '',
    not_dt_fecha_creacion       TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    not_v_usuario_creacion      VARCHAR(20) NOT NULL,
    not_dt_fecha_modificacion   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    not_v_usuario_modificacion  VARCHAR(20) NOT NULL,
    not_b_activo                BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_not_invitado   FOREIGN KEY (id_invitado) REFERENCES tt_invt_invitado(id_invitado)
);

-- 5.10) Orígenes adicionales
CREATE TABLE tt_orad_origen_adicional (
    id_origen_adicional         UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_origen_invitado          UUID        NOT NULL,
    id_campo_personalizado      BIGINT      NOT NULL,
    orad_v_valor                VARCHAR(20) NOT NULL DEFAULT '',
    orad_dt_fecha_registro_origen TIMESTAMP,
    orad_dt_fecha_modificacion_origen TIMESTAMP,
    orad_dt_fecha_creacion      TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orad_v_usuario_creacion     VARCHAR(20) NOT NULL,
    orad_dt_fecha_modificacion  TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orad_v_usuario_modificacion VARCHAR(20) NOT NULL,
    orad_b_activo               BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT fk_orad_origen_invitado FOREIGN KEY (id_origen_invitado) REFERENCES tt_orinvt_origen_invitado(id_origen_invitado),
    CONSTRAINT fk_orad_campo_personalizado FOREIGN KEY (id_campo_personalizado) REFERENCES tt_camp_campo_personalizado(id_campo_personalizado)
);

-- 5.11) Direcciones
CREATE TABLE tt_invtdir_invitado_direccion (
    id_invitado_direccion       UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_invitado                 UUID        NOT NULL,
    id_origen_invitado          UUID        NOT NULL,
    tcont_v_codigo              VARCHAR(5)  NOT NULL,
    id_distrito                 BIGINT,    -- FK a tm_dist_distrito(id_distrito)
    tvurb_v_codigo              VARCHAR(5)  NOT NULL,
    invtdir_v_coordenadas       VARCHAR(100) NOT NULL DEFAULT '',
    invtdir_v_numero_lote       VARCHAR(10) NOT NULL DEFAULT '',
    invtdir_i_piso              INT         NOT NULL DEFAULT 0,
    invtdir_i_departamento      INT         NOT NULL DEFAULT 0,
    invtdir_v_direccion         VARCHAR(100) NOT NULL DEFAULT '',
    invtdir_v_referencia        VARCHAR(100) NOT NULL DEFAULT '',
    invtdir_b_es_principal      BOOLEAN     NOT NULL DEFAULT FALSE,
    invtdir_b_prioridad         BOOLEAN     NOT NULL DEFAULT FALSE,
    invtdir_dt_fecha_creacion   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtdir_v_usuario_creacion  VARCHAR(20) NOT NULL,
    invtdir_dt_fecha_modificacion TIMESTAMP  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    invtdir_v_usuario_modificacion VARCHAR(20) NOT NULL,
    invtdir_b_activo            BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT fk_dir_invitado    FOREIGN KEY (id_invitado)        REFERENCES tt_invt_invitado(id_invitado),
    CONSTRAINT fk_dir_origen      FOREIGN KEY (id_origen_invitado) REFERENCES tt_orinvt_origen_invitado(id_origen_invitado),
    CONSTRAINT fk_dir_tipo_cont   FOREIGN KEY (tcont_v_codigo)      REFERENCES tm_tcont_tipo_contacto(tcont_v_codigo),
    CONSTRAINT fk_dir_distrito    FOREIGN KEY (id_distrito)         REFERENCES tm_dist_distrito(id_distrito),
    CONSTRAINT fk_dir_tvurb       FOREIGN KEY (tvurb_v_codigo)      REFERENCES tm_tvurb_tipo_via_urbana(tvurb_v_codigo)
);

-- 5.12) Orígenes de teléfono
CREATE TABLE tt_orinvttelf_origen_invitado_telefono (
    id_origen_invitado_telefono UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_invitado_telefono        UUID        NOT NULL,
    id_origen_invitado          UUID        NOT NULL,
    orinvttelf_b_es_principal   BOOLEAN     NOT NULL DEFAULT FALSE,
    orinvttelf_b_es_validado    BOOLEAN     NOT NULL DEFAULT FALSE,
    orinvttelf_v_codigo_validacion VARCHAR(20) NOT NULL DEFAULT '',
    orinvttelf_dt_fecha_creacion TIMESTAMP  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orinvttelf_v_usuario_creacion VARCHAR(20) NOT NULL,
    orinvttelf_dt_fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orinvttelf_v_usuario_modificacion VARCHAR(20) NOT NULL,
    orinvttelf_b_activo         BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT fk_oritel_invitado_tel FOREIGN KEY (id_invitado_telefono)    REFERENCES tt_invttelf_invitado_telefono(id_invitado_telefono),
    CONSTRAINT fk_oritel_origen        FOREIGN KEY (id_origen_invitado)     REFERENCES tt_orinvt_origen_invitado(id_origen_invitado)
);

-- 5.13) Orígenes de correo
CREATE TABLE tt_orinvtcor_origen_invitado_correo (
    id_origen_invitado_correo   UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_invitado_correo          UUID        NOT NULL,
    id_origen_invitado          UUID        NOT NULL,
    orinvtcor_b_es_validado     BOOLEAN     NOT NULL DEFAULT FALSE,
    orinvtcor_v_codigo_validacion VARCHAR(20) NOT NULL DEFAULT '',
    orinvtcor_b_es_principal    BOOLEAN     NOT NULL DEFAULT FALSE,
    orinvtcor_dt_fecha_creacion TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orinvtcor_v_usuario_creacion VARCHAR(20) NOT NULL,
    orinvtcor_dt_fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orinvtcor_v_usuario_modificacion VARCHAR(20) NOT NULL,
    orinvtcor_b_activo          BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT fk_oricor_invitado_correo FOREIGN KEY (id_invitado_correo)    REFERENCES tt_invtcor_invitado_correo(id_invitado_correo),
    CONSTRAINT fk_oricor_origen           FOREIGN KEY (id_origen_invitado)  REFERENCES tt_orinvt_origen_invitado(id_origen_invitado)
);

-- 5.14) Orígenes de documento
CREATE TABLE tt_orinvtdoc_origen_invitado_documento (
    id_origen_invitado_documento UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_invitado_documento       UUID        NOT NULL,
    id_origen_invitado          UUID        NOT NULL,
    orinvtdoc_b_es_principal    BOOLEAN     NOT NULL DEFAULT FALSE,
    orinvtdoc_dt_fecha_creacion TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orinvtdoc_v_usuario_creacion VARCHAR(20) NOT NULL,
    orinvtdoc_dt_fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    orinvtdoc_v_usuario_modificacion VARCHAR(20) NOT NULL,
    orinvtdoc_b_activo          BOOLEAN     NOT NULL DEFAULT FALSE,
    CONSTRAINT fk_oridoc_invitado_doc FOREIGN KEY (id_invitado_documento)    REFERENCES tt_invtdoc_invitado_documento(id_invitado_documento),
    CONSTRAINT fk_oridoc_origen         FOREIGN KEY (id_origen_invitado)       REFERENCES tt_orinvt_origen_invitado(id_origen_invitado)
);

-- 5.15) Correos de notificación
CREATE TABLE tt_cnotif_correo_notificacion (
    id_correo_notificacion      BIGINT      NOT NULL PRIMARY KEY,
    id_invitado_correo          UUID        NOT NULL,
    tnotif_v_codigo             VARCHAR(10) NOT NULL, -- FK a tm_tnotif_tipo_notificacion(tnotif_v_codigo)
    cnotif_dt_fecha_creacion    TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    cnotif_v_usuario_creacion   VARCHAR(20) NOT NULL,
    cnotif_dt_fecha_modificacion TIMESTAMP  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    cnotif_v_usuario_modificacion VARCHAR(20) NOT NULL,
    cnotif_b_activo             BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_cnotif_invitado_correo FOREIGN KEY (id_invitado_correo) REFERENCES tt_invtcor_invitado_correo(id_invitado_correo),
    CONSTRAINT fk_cnotif_tipo_notificacion FOREIGN KEY (tnotif_v_codigo) REFERENCES tm_tnotif_tipo_notificacion(tnotif_v_codigo)
);

-- 5.16) Telefonía de notificación
CREATE TABLE tt_tnotif_telefono_notificacion (
    id_telefono_notificacion    BIGINT      NOT NULL PRIMARY KEY,
    id_invitado_telefono        UUID        NOT NULL,
    mnotif_v_codigo             VARCHAR(10) NOT NULL, -- FK a tm_mnotif_medio_notificacion(mnotif_v_codigo)
    tnotif_v_codigo             VARCHAR(10) NOT NULL, -- FK a tm_tnotif_tipo_notificacion(tnotif_v_codigo)
    tnotif_dt_fecha_creacion    TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    tnotif_v_usuario_creacion   VARCHAR(20) NOT NULL,
    tnotif_dt_fecha_modificacion TIMESTAMP  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    tnotif_v_usuario_modificacion VARCHAR(20) NOT NULL,
    tnotif_b_activo             BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_telnotif_invitado_tel FOREIGN KEY (id_invitado_telefono) REFERENCES tt_invttelf_invitado_telefono(id_invitado_telefono),
    CONSTRAINT fk_telnotif_medio_notificacion FOREIGN KEY (mnotif_v_codigo) REFERENCES tm_mnotif_medio_notificacion(mnotif_v_codigo),
    CONSTRAINT fk_telnotif_tipo_notificacion FOREIGN KEY (tnotif_v_codigo) REFERENCES tm_tnotif_tipo_notificacion(tnotif_v_codigo)
);
