using Domain.Entities.Invitados.Invitados;
using Domain.ValueObjects;

namespace Domain.Entities.Invitados.InvitadoPreferencias
{
    public class InvitadoPreferencia : EntityBase
    {
        public long Id { get; private set; }
        public Guid InvitadoId { get; private set; }
        public long CategoriaPreferenciaId { get; private set; }
        public string Valor { get; private set; }
        public bool LeGusta { get; private set; }
        public Invitado Invitado { get; private set; }

        public InvitadoPreferencia(
            Guid invitadoId,
            long categoriaId,
            string valor,
            bool leGusta)
        {

            InvitadoId = invitadoId;
            CategoriaPreferenciaId = categoriaId;
            Valor = valor;
            LeGusta = leGusta;
        }
        private InvitadoPreferencia() { }
    }
}
