using Domain.Entities.Invitados.EtiquetaInvitados;
using Domain.Entities.Invitados.InvitadoPreferencias;
using Domain.Entities.Invitados.Notas;
using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Invitados; 
public class Invitado : EntityBase
{
    public Guid Id { get; private set; }
    public string PrimerNombre { get; private set; }
    public string SegundoNombre { get; private set; }
    public string ApellidoPaterno { get; private set; }
    public string ApellidoMaterno { get; private set; }
    public long? EstadoCivilId { get; private set; }
    public DateTime? FechaNacimiento { get; private set; }
    public long? GeneroId { get; private set; }
    public string PaisCodigo { get; private set; }
    public ICollection<InvitadoEtiqueta> Etiquetas { get; } = new List<InvitadoEtiqueta>();
    public ICollection<InvitadoPreferencia> Preferencias { get; } = new List<InvitadoPreferencia>();
    public ICollection<InvitadoNota> InvitadoNotas { get; } = new List<InvitadoNota>();
    private Invitado() { }

    public Invitado(
        string primerNombre,
        string segundoNombre,
        string apellidoPaterno,
        string apellidoMaterno,
        long? estadoCivilId,
        DateTime? fechaNacimiento,
        long? generoId,
        string paisCodigo)
    {
        Id = Guid.NewGuid();
        if (string.IsNullOrWhiteSpace(primerNombre)) throw new ArgumentException("El primer nombre es obligatorio.", nameof(primerNombre));
        if (string.IsNullOrWhiteSpace(apellidoPaterno)) throw new ArgumentException("El apellido paterno es obligatorio.", nameof(apellidoPaterno));
        if (string.IsNullOrWhiteSpace(paisCodigo)) throw new ArgumentException("El código de país es obligatorio.", nameof(paisCodigo));

        PrimerNombre = primerNombre;
        SegundoNombre = segundoNombre;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        EstadoCivilId = estadoCivilId;
        FechaNacimiento = fechaNacimiento;
        GeneroId = generoId;
        PaisCodigo = paisCodigo;
    }
}

