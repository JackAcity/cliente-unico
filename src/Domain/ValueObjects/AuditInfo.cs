
namespace Domain.ValueObjects;

public partial record AuditInfo
{
    public DateTime FechaModificacion { get; private set; }
    public string UsuarioModificacion { get; private set; }
    public bool Activo { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public string UsuarioCreacion { get; private set; }

    private AuditInfo() { }

    private AuditInfo(
           DateTime fechaCreacion,
           string usuarioCreacion,
           DateTime fechaModificacion,
           string usuarioModificacion,
           bool activo)
    {
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        FechaModificacion = fechaModificacion;
        UsuarioModificacion = usuarioModificacion;
        Activo = activo;
    }


    public static AuditInfo? Create(string usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario))
            return null;

        var now = DateTime.UtcNow;
        return new AuditInfo(
            fechaCreacion: now,
            usuarioCreacion: usuario,
            fechaModificacion: now,
            usuarioModificacion: usuario,
            activo: true
        );
    }
    public  AuditInfo? Update(string usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario))
            return null;

        var now = DateTime.UtcNow;
        return new AuditInfo(
            fechaCreacion: FechaCreacion,
            usuarioCreacion: UsuarioCreacion,
            fechaModificacion: now,
            usuarioModificacion: usuario,
            activo: true
        );
    }

}

