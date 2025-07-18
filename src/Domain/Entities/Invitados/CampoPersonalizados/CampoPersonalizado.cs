using Domain.ValueObjects;

namespace Domain.Entities.Invitados.CampoPersonalizados
{
    public class CampoPersonalizado : EntityBase
    {
        public long Id { get; private set; }
        public string Nombre { get; private set; }
        public bool Visualizar { get; private set; }

        public CampoPersonalizado(
            string nombre, bool visualizar)
        {
            Nombre = nombre;
            Visualizar = visualizar;
        }
        private CampoPersonalizado() { }


    }
}
