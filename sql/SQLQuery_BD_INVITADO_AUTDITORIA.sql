-- 1) Eliminación y creación de la base de datos
DROP DATABASE IF EXISTS bd_invitado_auditoria;
CREATE DATABASE bd_invitado_auditoria;

USE bd_invitado_auditoria;

-- 3.1) Catalogo de etapas
CREATE TABLE tm_et_etapa (
    id_etapa                   BIGINT      NOT NULL PRIMARY KEY,
    et_nombre                  VARCHAR(20) NOT NULL,
    et_dt_fecha_creacion       TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    et_v_usuario_creacion      VARCHAR(20) NOT NULL,
    et_dt_fecha_modificacion   TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    et_v_usuario_modificacion  VARCHAR(20) NOT NULL,
    et_b_activo                BOOLEAN     NOT NULL DEFAULT TRUE
);

-- 3.2) Trazabilidad de mensajes/operaciones
CREATE TABLE tr_traza_trazabilidad (
    id_trazabilidad            UUID        NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_sistema                 BIGINT,
    traza_j_trama              JSON,
    traza_i_accion             INT,
    id_etapa                   BIGINT,
    traza_dt_fecha_creacion    TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
    traza_v_usuario_creacion   VARCHAR(20) NOT NULL,
    traza_dt_fecha_modificacion TIMESTAMP  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    traza_v_usuario_modificacion VARCHAR(20) NOT NULL,
    traza_b_activo             BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_traza_etapa
      FOREIGN KEY (id_etapa)
      REFERENCES tm_et_etapa(id_etapa)
);

-- 3.3) Auditoría de integración
CREATE TABLE tr_audit_auditoria_integracion (
    id_auditoria_integracion       UUID      NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
    id_sistema                     BIGINT    NOT NULL,
    audit_v_log                    TEXT,                  -- log libremente largo
    id_trazabilidad                UUID,
    audit_v_ubicacion              VARCHAR(100),
    id_sistema_origen_invitado     VARCHAR(100),
    audit_dt_fecha_creacion        TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    audit_v_usuario_creacion       VARCHAR(20) NOT NULL,
    audit_dt_fecha_modificacion    TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    audit_v_usuario_modificacion   VARCHAR(20) NOT NULL,
    audit_b_activo                 BOOLEAN  NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_audit_trazabilidad
      FOREIGN KEY (id_trazabilidad)
      REFERENCES tr_traza_trazabilidad(id_trazabilidad)
);
