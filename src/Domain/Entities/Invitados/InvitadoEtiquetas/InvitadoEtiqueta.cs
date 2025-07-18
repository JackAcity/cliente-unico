using Domain.Entities.Invitados.Invitados;
using Domain.ValueObjects;

namespace Domain.Entities.Invitados.EtiquetaInvitados
{
    public class InvitadoEtiqueta : EntityBase
    {
        public long Id { get; private set; }
        public long EtiquetaId { get; private set; }
        public Guid InvitadoId { get; private set; }
        public Invitado Invitado { get; private set; }
        public InvitadoEtiqueta(
            long etiquetaId, 
            Guid invitadoId)
        {
            EtiquetaId = etiquetaId;
            InvitadoId = invitadoId;
        }
        private InvitadoEtiqueta() { }
    }
}
