using Domain.ValueObjects;

namespace Domain.Entities.Invitados.DatosPrincipales
{
    public class InvitadoDatosPrincipales : EntityBase
    {
        public Guid Id { get; private set; }
        public string TipoDocumentoCodigo { get; private set; }
        public string DocumentoIdentidad { get; private set; }
        public string Correo { get; private set; }
        public string Numero { get; private set; }
        public string Direccion { get; private set; }
        public string Referencia { get; private set; }
        public Guid InvitadoId { get; private set; }

        private InvitadoDatosPrincipales() { }
        public InvitadoDatosPrincipales(
            string tipoDocCodigo,
            string docIdentidad,
            string correo,
            string numero,
            string direccion,
            string referencia,
            Guid invitadoId)
           
        {
            Id = Guid.NewGuid();
            TipoDocumentoCodigo = tipoDocCodigo;
            DocumentoIdentidad = docIdentidad;
            Correo = correo;
            Numero = numero;
            Direccion = direccion;
            Referencia = referencia;
            InvitadoId = invitadoId;
        }

    }
}
