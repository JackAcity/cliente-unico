using Domain.Entities.Invitados.Correos;

namespace Domain.Entities.Invitados.CorreoNotificaciones
{
    public class CorreoNotificacion : EntityBase
    {
        public long Id { get; private set; }
        public Guid InvitadoCorreoId { get; private set; }
        public string TipoNotificacionCodigo { get; private set; }
        public InvitadoCorreo InvitadoCorreo { get; private set; }
        public CorreoNotificacion(
            Guid invitadoCorreoId, 
            string tipoNotifCodigo)
        {
            InvitadoCorreoId = invitadoCorreoId;
            TipoNotificacionCodigo = tipoNotifCodigo;
        }
        private CorreoNotificacion() { }
    }
}
