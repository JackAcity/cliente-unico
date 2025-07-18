
namespace Domain.Entities.Invitados.Telefonos
{
    public class InvitadoTelefono : EntityBase
    {
        public Guid Id { get; private set; }
        public Guid InvitadoId { get; private set; }
        public string TipoContactoCodigo { get; private set; }
        public string PaisPrefijo { get; private set; }
        public string Numero { get; private set; }
        public bool DeseaNotificacion { get; private set; }

        public InvitadoTelefono(
            Guid invitadoId,
            string tipoContCodigo,
            string paisPrefijo,
            string numero,
            bool deseaNotificacion
            )
        {
            Id = Guid.NewGuid();
            InvitadoId = invitadoId;
            TipoContactoCodigo = tipoContCodigo;
            PaisPrefijo = paisPrefijo;
            Numero = numero;
            DeseaNotificacion = deseaNotificacion;
        }
        private InvitadoTelefono() { }
    }
}
