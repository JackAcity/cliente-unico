using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Origenes
{
    public class OrigenInvitadoDocumento : EntityBase
    {
        public Guid Id { get; private set; }
        public Guid InvitadoDocumentoId { get; private set; }
        public Guid OrigenInvitadoId { get; private set; }
        public bool EsPrincipal { get; private set; }

        public OrigenInvitadoDocumento(
            Guid invitadoDocumentoId,
            Guid origenInvitadoId,
            bool esPrincipal)
        {
            Id = Guid.NewGuid();
            InvitadoDocumentoId = invitadoDocumentoId;
            OrigenInvitadoId = origenInvitadoId;
            EsPrincipal = esPrincipal;
        }
        private OrigenInvitadoDocumento() { }
    }
}
