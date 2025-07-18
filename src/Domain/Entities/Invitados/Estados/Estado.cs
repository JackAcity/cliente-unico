using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Estados
{
    public class Estado : EntityBase
    {
        public long Id { get; private set; }
        public string Nombre { get; private set; }

        public Estado( string nombre)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }
        private Estado() { }
    }
}
