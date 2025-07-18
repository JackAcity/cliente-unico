using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Origenes
{
    public class OrigenInvitadoCorreo : EntityBase
    {
        public Guid Id { get; private set; }
        public Guid InvitadoCorreoId { get; private set; }
        public Guid OrigenInvitadoId { get; private set; }
        public bool EsValidado { get; private set; }
        public string CodigoValidacion { get; private set; }
        public bool EsPrincipal { get; private set; }

        public OrigenInvitadoCorreo(
            Guid invitadoCorreoId,
            Guid origenInvitadoId,
            bool esValidado,
            string codigoValidacion,
            bool esPrincipal)
        {
            Id = Guid.NewGuid();
            InvitadoCorreoId = invitadoCorreoId;
            OrigenInvitadoId = origenInvitadoId;
            EsValidado = esValidado;
            CodigoValidacion = codigoValidacion;
            EsPrincipal = esPrincipal;
        }
        private OrigenInvitadoCorreo() { }
    }
}
