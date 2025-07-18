using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Etiquetas
{
    public class Etiqueta : EntityBase
    {
        public long Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public bool EsAutomatico { get; private set; }

        public Etiqueta( string nombre, string descripcion, bool esAutomatico)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            EsAutomatico = esAutomatico;
        }
        private Etiqueta() { }
    }
}
