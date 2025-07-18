using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.Invitados.CategoriaPreferencias
{
    public class CategoriaPreferencia : EntityBase
    {
        public long Id { get; private set; }
        public string Nombre { get; private set; }

        public CategoriaPreferencia(long id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        private CategoriaPreferencia() { }
    }
}
