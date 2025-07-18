using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Origenes
{
    public class OrigenAdicional : EntityBase
    {
        public Guid Id { get; private set; }
        public Guid OrigenInvitadoId { get; private set; }
        public long CampoPersonalizadoId { get; private set; }
        public string Valor { get; private set; }

        public OrigenAdicional(
            Guid origenInvitadoId,
            long campoPersonalizadoId,
            string valor)
        {
            Id = Guid.NewGuid();
            OrigenInvitadoId = origenInvitadoId;
            CampoPersonalizadoId = campoPersonalizadoId;
            Valor = valor;
        }
        private OrigenAdicional() { }
    }
}
