using Domain.Entities.Invitados.Invitados;
using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Notas
{
    public class InvitadoNota : EntityBase
    {
        public long Id { get; private set; }
        public Guid InvitadoId { get; private set; }
        public string Comentario { get; private set; }
        public Invitado Invitado { get; private set; }
        public InvitadoNota( Guid invitadoId, string comentario)
        {
            InvitadoId = invitadoId;
            Comentario = comentario;
        }
        private InvitadoNota() { }
    }

}
