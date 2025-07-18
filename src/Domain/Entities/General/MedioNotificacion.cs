using Domain.ValueObjects;

namespace Domain.Entities.General
{
    public class MedioNotificacion : EntityBase
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }

        public AuditInfo AuditInfo { get; private set; }

        public MedioNotificacion(string codigo, string nombre, AuditInfo auditInfo)
            : base(auditInfo)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
    }


    public class TipoNotificacion : EntityBase
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public string? CodigoPadre { get; private set; }

        public AuditInfo AuditInfo { get; private set; }

        public TipoNotificacion(string codigo, string nombre, string? codigoPadre, AuditInfo auditInfo) : base(auditInfo)
        {
            Codigo = codigo;
            Nombre = nombre;
            CodigoPadre = codigoPadre;
        }
    }

    public class Moneda : EntityBase
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public string Simbolo { get; private set; }

        public AuditInfo AuditInfo { get; private set; }

        public Moneda(string codigo, string nombre, string simbolo, AuditInfo auditInfo) : base(auditInfo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Simbolo = simbolo;
        }
    }
    public class TipoViaUrbana : EntityBase
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }

        public AuditInfo AuditInfo { get; private set; }

        public TipoViaUrbana(string codigo, string nombre, AuditInfo auditInfo) : base(auditInfo)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
    }
    public class Pais : EntityBase
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public string Prefijo { get; private set; }
        public string Icono { get; private set; }
        public string Nacionalidad { get; private set; }

        public AuditInfo AuditInfo { get; private set; }

        public Pais(string codigo, string nombre, string prefijo, string icono, string nacionalidad, AuditInfo auditInfo) : base(auditInfo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Prefijo = prefijo;
            Icono = icono;
            Nacionalidad = nacionalidad;
        }
    }

    public class Departamento : EntityBase
    {
        public long Id { get; private set; }
        public string Nombre { get; private set; }
        public string CodigoUbigeo { get; private set; }

        public AuditInfo AuditInfo { get; private set; }

        public Departamento(long id, string nombre, string codigoUbigeo, AuditInfo auditInfo) : base(auditInfo)
        {
            Id = id;
            Nombre = nombre;
            CodigoUbigeo = codigoUbigeo;
        }
    }
    public class Provincia : EntityBase
    {
        public long Id { get; private set; }
        public string Nombre { get; private set; }
        public string CodigoUbigeo { get; private set; }
        public long DepartamentoId { get; private set; }

        public AuditInfo AuditInfo { get; private set; }

        public Provincia(long id, string nombre, string codigoUbigeo, long departamentoId, AuditInfo auditInfo) : base(auditInfo)
        {
            Id = id;
            Nombre = nombre;
            CodigoUbigeo = codigoUbigeo;
            DepartamentoId = departamentoId;
            AuditInfo = auditInfo;
        }
    }
    public class Distrito : EntityBase
    {
        public long Id { get; private set; }
        public string Nombre { get; private set; }
        public string CodigoUbigeo { get; private set; }
        public long ProvinciaId { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public Distrito(long id, string nombre, string codigoUbigeo, long provinciaId, AuditInfo auditInfo) : base(auditInfo)
        {
            Id = id;
            Nombre = nombre;
            CodigoUbigeo = codigoUbigeo;
            ProvinciaId = provinciaId;
        }
    }

    public class TipoContacto : EntityBase
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public TipoContacto(string codigo, string nombre, AuditInfo auditInfo) : base(auditInfo)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
    }
    public class TipoDocumento : EntityBase
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public TipoDocumento(string codigo, string nombre, AuditInfo auditInfo) : base(auditInfo)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
    }
    public class Sistema : EntityBase
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public int? Prioridad { get; private set; }
        public bool? SincronizarDatos { get; private set; }
        public int? TipoProceso { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public Sistema(string codigo, string nombre, int? prioridad, bool? sincronizarDatos, int? tipoProceso, AuditInfo auditInfo) : base(auditInfo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Prioridad = prioridad;
            SincronizarDatos = sincronizarDatos;
            TipoProceso = tipoProceso;
        }

    }
    public class EstadoCivil : EntityBase
    {
        public long Id { get; private set; }
        public string Nombre { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public EstadoCivil(long id, string nombre, AuditInfo auditInfo) : base(auditInfo)
        {
            Id = id;
            Nombre = nombre;
        }

    }
    public class Genero : EntityBase
    {
        public long Id { get; private set; }
        public string Nombre { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public Genero(long id, string nombre, AuditInfo auditInfo) : base(auditInfo)
        {
            Id = id;
            Nombre = nombre;
        }

    }

}
