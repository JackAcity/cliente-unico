-- Eliminación y creación de la base de datos
DROP DATABASE IF EXISTS bd_general;
CREATE DATABASE bd_general;
USE bd_general;

-- Tabla: tm_mnotif_medio_notificacion
CREATE TABLE tm_mnotif_medio_notificacion (
    mnotif_v_codigo               VARCHAR(5)   NOT NULL PRIMARY KEY,
    mnotif_v_nombre               VARCHAR(50)  NOT NULL DEFAULT '',
    mnotif_dt_fecha_creacion      TIMESTAMP    NOT NULL,
    mnotif_v_usuario_creacion     VARCHAR(20)  NOT NULL,
    mnotif_dt_fecha_modificacion  TIMESTAMP    NOT NULL,
    mnotif_v_usuario_modificacion VARCHAR(20)  NOT NULL,
    b_activo                      BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_tnotif_tipo_notificacion
CREATE TABLE tm_tnotif_tipo_notificacion (
    tnotif_v_codigo               VARCHAR(10)  NOT NULL PRIMARY KEY,
    tnotif_v_nombre               VARCHAR(50)  NOT NULL DEFAULT '',
    tnotif_v_codigo_padre         VARCHAR(10),
    tnotif_dt_fecha_creacion      TIMESTAMP    NOT NULL,
    tnotif_v_usuario_creacion     VARCHAR(20)  NOT NULL,
    tnotif_dt_fecha_modificacion  TIMESTAMP    NOT NULL,
    tnotif_v_usuario_modificacion VARCHAR(20)  NOT NULL,
    b_activo                      BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_mond_moneda
CREATE TABLE tm_mond_moneda (
    mond_v_codigo                 VARCHAR(5)   NOT NULL PRIMARY KEY,
    mond_v_nombre                 VARCHAR(50)  NOT NULL DEFAULT '',
    mond_v_simbolo                VARCHAR(2)   NOT NULL DEFAULT '',
    mond_dt_fecha_creacion        TIMESTAMP    NOT NULL,
    mond_v_usuario_creacion       VARCHAR(20)  NOT NULL,
    mond_dt_fecha_modificacion    TIMESTAMP    NOT NULL,
    mond_v_usuario_modificacion   VARCHAR(20)  NOT NULL,
    mond_b_activo                 BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_tvurb_tipo_via_urbana
CREATE TABLE tm_tvurb_tipo_via_urbana (
    tvurb_v_codigo                VARCHAR(5)   NOT NULL PRIMARY KEY,
    tvurb_v_nombre                VARCHAR(50)  NOT NULL DEFAULT '',
    tvurb_dt_fecha_creacion       TIMESTAMP    NOT NULL,
    tvurb_v_usuario_creacion      VARCHAR(20)  NOT NULL,
    tvurb_dt_fecha_modificacion   TIMESTAMP    NOT NULL,
    tvurb_v_usuario_modificacion  VARCHAR(20)  NOT NULL,
    tvurb_b_activo                BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_pa_pais
CREATE TABLE tm_pa_pais (
    pa_v_codigo                   VARCHAR(10)  NOT NULL PRIMARY KEY,
    pa_v_nombre                   VARCHAR(50)  NOT NULL DEFAULT '',
    pa_v_prefijo                  VARCHAR(5)   NOT NULL UNIQUE,
    pa_v_icono                    VARCHAR(100) NOT NULL DEFAULT '',
    pa_v_nacionalidad             VARCHAR(100) NOT NULL DEFAULT '',
    pa_dt_fecha_creacion          TIMESTAMP    NOT NULL,
    pa_v_usuario_creacion         VARCHAR(20)  NOT NULL,
    pa_dt_fecha_modificacion      TIMESTAMP    NOT NULL,
    pa_v_usuario_modificacion     VARCHAR(20)  NOT NULL,
    b_activo                      BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_tcont_tipo_contacto
CREATE TABLE tm_tcont_tipo_contacto (
    tcont_v_codigo                VARCHAR(5)   NOT NULL PRIMARY KEY,
    tcont_v_nombre                VARCHAR(50)  NOT NULL DEFAULT '',
    tcont_dt_fecha_creacion       TIMESTAMP    NOT NULL,
    tcont_v_usuario_creacion      VARCHAR(20)  NOT NULL,
    tcont_dt_fecha_modificacion   TIMESTAMP    NOT NULL,
    tcont_v_usuario_modificacion  VARCHAR(20)  NOT NULL,
    b_activo                      BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_tdoc_tipo_documento
CREATE TABLE tm_tdoc_tipo_documento (
    tdoc_v_codigo                 VARCHAR(10)  NOT NULL PRIMARY KEY,
    tdoc_v_nombre                 VARCHAR(50)  NOT NULL DEFAULT '',
    tdoc_dt_fecha_creacion        TIMESTAMP    NOT NULL,
    tdoc_v_usuario_creacion       VARCHAR(20)  NOT NULL,
    tdoc_dt_fecha_modificacion    TIMESTAMP    NOT NULL,
    tdoc_v_usuario_modificacion   VARCHAR(20)  NOT NULL,
    tdoc_b_activo                 BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_sis_sistema
CREATE TABLE tm_sis_sistema (
    sis_v_codigo                  VARCHAR(10)  NOT NULL PRIMARY KEY,
    sis_v_nombre                  VARCHAR(50)  NOT NULL DEFAULT '',
    sis_i_prioridad               SMALLINT,
    sis_b_sincronizar_datos       BOOLEAN,
    sis_i_tipo_proceso            INT,
    sis_dt_fecha_creacion         TIMESTAMP    NOT NULL,
    sis_v_usuario_creacion        VARCHAR(20)  NOT NULL,
    sis_dt_fecha_modificacion     TIMESTAMP    NOT NULL,
    sis_v_usuario_modificacion    VARCHAR(20)  NOT NULL,
    sis_b_activo                  BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_escv_estado_civil
CREATE TABLE tm_escv_estado_civil (
    id_estado_civil               BIGINT       NOT NULL PRIMARY KEY,
    escv_v_nombre                 VARCHAR(50)  NOT NULL DEFAULT '',
    escv_dt_fecha_creacion        TIMESTAMP    NOT NULL,
    escv_v_usuario_creacion       VARCHAR(20)  NOT NULL,
    escv_dt_fecha_modificacion    TIMESTAMP    NOT NULL,
    escv_v_usuario_modificacion   VARCHAR(20)  NOT NULL,
    escv_b_activo                 BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_gen_genero
CREATE TABLE tm_gen_genero (
    id_genero                     BIGINT       NOT NULL PRIMARY KEY,
    gen_v_nombre                  VARCHAR(50)  NOT NULL DEFAULT '',
    gen_dt_fecha_creacion         TIMESTAMP    NOT NULL,
    gen_v_usuario_creacion        VARCHAR(20)  NOT NULL,
    gen_dt_fecha_modificacion     TIMESTAMP    NOT NULL,
    gen_v_usuario_modificacion    VARCHAR(20)  NOT NULL,
    gen_b_activo                  BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_dep_departamento
CREATE TABLE tm_dep_departamento (
    id_departamento               BIGINT       NOT NULL PRIMARY KEY,
    dep_v_nombre                  VARCHAR(50)  NOT NULL DEFAULT '',
    dep_v_codigo_ubigeo           VARCHAR(3)   NOT NULL DEFAULT '',
    dep_dt_fecha_creacion         TIMESTAMP    NOT NULL,
    dep_v_usuario_creacion        VARCHAR(20)  NOT NULL,
    dep_dt_fecha_modificacion     TIMESTAMP    NOT NULL,
    dep_v_usuario_modificacion    VARCHAR(20)  NOT NULL,
    dep_b_activo                  BOOLEAN      NOT NULL DEFAULT TRUE
);

-- Tabla: tm_prov_provincia
CREATE TABLE tm_prov_provincia (
    id_provincia                  BIGINT       NOT NULL PRIMARY KEY,
    prov_v_nombre                 VARCHAR(50)  NOT NULL DEFAULT '',
    prov_v_codigo_ubigeo          VARCHAR(3)   NOT NULL DEFAULT '',
    id_departamento               BIGINT,
    prov_dt_fecha_creacion        TIMESTAMP    NOT NULL,
    prov_v_usuario_creacion       VARCHAR(20)  NOT NULL,
    prov_dt_fecha_modificacion    TIMESTAMP    NOT NULL,
    prov_v_usuario_modificacion   VARCHAR(20)  NOT NULL,
    prov_b_activo                 BOOLEAN      NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_departamento) REFERENCES tm_dep_departamento(id_departamento)
);

-- Tabla: tm_dist_distrito
CREATE TABLE tm_dist_distrito (
    id_distrito                   BIGINT       NOT NULL PRIMARY KEY,
    dist_v_nombre                 VARCHAR(50)  NOT NULL DEFAULT '',
    id_provincia                  BIGINT,
    dist_v_codigo_ubigeo          VARCHAR(3)   NOT NULL DEFAULT '',
    dist_dt_fecha_creacion        TIMESTAMP    NOT NULL,
    dist_v_usuario_creacion       VARCHAR(20)  NOT NULL,
    dist_dt_fecha_modificacion    TIMESTAMP    NOT NULL,
    dist_v_usuario_modificacion   VARCHAR(20)  NOT NULL,
    dist_b_activo                 BOOLEAN      NOT NULL DEFAULT TRUE,
    FOREIGN KEY (id_provincia) REFERENCES tm_prov_provincia(id_provincia)
);
