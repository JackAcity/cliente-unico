using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Telefonos
{
    public class TelefonoNotificacion : EntityBase
    {
        public long Id { get; private set; }
        public Guid InvitadoTelefonoId { get; private set; }
        public string MedioNotificacionCodigo { get; private set; }
        public string TipoNotificacionCodigo { get; private set; }

        public TelefonoNotificacion(
            Guid invitadoTelefonoId,
            string medioNotifCodigo,
            string tipoNotifCodigo)
        {
            InvitadoTelefonoId = invitadoTelefonoId;
            MedioNotificacionCodigo = medioNotifCodigo;
            TipoNotificacionCodigo = tipoNotifCodigo;
        }
        private TelefonoNotificacion() { }
    }
}
