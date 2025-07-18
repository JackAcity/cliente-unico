using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Origenes
{
    public class OrigenInvitado : EntityBase
    {
        public Guid Id { get; private set; }
        public string SistemaCodigo { get; private set; }
        public string OrigenId { get; private set; }
        public DateTime? FechaRegistroOrigen { get; private set; }
        public DateTime? FechaModificaOrigen { get; private set; }
        public Guid InvitadoId { get; private set; }
        public long? EstadoId { get; private set; }

        public OrigenInvitado(
            string sistemaCodigo,
            string origenId,
            DateTime? fechaRegistro,
            DateTime? fechaModifica,
            Guid invitadoId,
            long? estadoId)
        {
            Id = Guid.NewGuid();
            SistemaCodigo = sistemaCodigo;
            OrigenId = origenId;
            FechaRegistroOrigen = fechaRegistro;
            FechaModificaOrigen = fechaModifica;
            InvitadoId = invitadoId;
            EstadoId = estadoId;
        }
        private OrigenInvitado() { }
    }

}
