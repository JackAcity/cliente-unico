
using Domain.Entities.Invitados.CorreoNotificaciones;

namespace Domain.Entities.Invitados.Correos 
{
    public class InvitadoCorreo : EntityBase
    {
        public Guid Id { get; private set; }
        public Guid InvitadoId { get; private set; }
        public string TipoContactoCodigo { get; private set; }
        public string Correo { get; private set; }
        public bool DeseaNotificacion { get; private set; }
        public bool EsPrioridad { get; private set; }
        public ICollection<CorreoNotificacion> CorreosNotificacion { get; private set; }
       = new List<CorreoNotificacion>();
        public InvitadoCorreo(
            Guid invitadoId,
            string tipoContCodigo,
            string correo,
            bool deseaNot,
            bool esPrioridad
            )
         
        {
            Id = Guid.NewGuid();
            InvitadoId = invitadoId;
            TipoContactoCodigo = tipoContCodigo;
            Correo = correo;
            DeseaNotificacion = deseaNot;
            EsPrioridad = esPrioridad;
          
               
        }
        private InvitadoCorreo() { }
      

    }
}
