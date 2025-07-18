using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Direcciones
{
    public class InvitadoDireccion : EntityBase
    {
        public Guid Id { get; private set; }
        public Guid InvitadoId { get; private set; }
        public string TipoContactoCodigo { get; private set; }
        public long? DistritoId { get; private set; }
        public string TipoViaCodigo { get; private set; }
        public string Coordenadas { get; private set; }
        public string NumeroLote { get; private set; }
        public int Piso { get; private set; }
        public int Departamento { get; private set; }
        public string Direccion { get; private set; }
        public string Referencia { get; private set; }
        public bool EsPrincipal { get; private set; }
        public bool Prioridad { get; private set; }

        public InvitadoDireccion(
            Guid invitadoId,
            string tipoContCodigo,
            long? distritoId,
            string tipoViaCodigo,
            string coordenadas,
            string numeroLote,
            int piso,
            int departamento,
            string direccion,
            string referencia,
            bool esPrincipal,
            bool prioridad)
        {
            Id = Guid.NewGuid();
            InvitadoId = invitadoId;
            TipoContactoCodigo = tipoContCodigo;
            DistritoId = distritoId;
            TipoViaCodigo = tipoViaCodigo;
            Coordenadas = coordenadas;
            NumeroLote = numeroLote;
            Piso = piso;
            Departamento = departamento;
            Direccion = direccion;
            Referencia = referencia;
            EsPrincipal = esPrincipal;
            Prioridad = prioridad;
        }
        private InvitadoDireccion() { }
    }
}
