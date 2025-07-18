using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Origenes
{
    public class OrigenInvitadoTelefono : EntityBase
    {
        public Guid Id { get; private set; }
        public Guid InvitadoTelefonoId { get; private set; }
        public Guid OrigenInvitadoId { get; private set; }
        public bool EsPrincipal { get; private set; }
        public bool EsValidado { get; private set; }
        public string CodigoValidacion { get; private set; }

        public OrigenInvitadoTelefono(
            Guid invitadoTelefonoId,
            Guid origenInvitadoId,
            bool esPrincipal,
            bool esValidado,
            string codigoValidacion)
        {
            Id = Guid.NewGuid();
            InvitadoTelefonoId = invitadoTelefonoId;
            OrigenInvitadoId = origenInvitadoId;
            EsPrincipal = esPrincipal;
            EsValidado = esValidado;
            CodigoValidacion = codigoValidacion;
        }
        private OrigenInvitadoTelefono() { }
    }
}
