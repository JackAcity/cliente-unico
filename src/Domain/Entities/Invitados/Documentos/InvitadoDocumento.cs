using Domain.ValueObjects;

namespace Domain.Entities.Invitados.Documentos
{
    public class InvitadoDocumento : EntityBase
    {
        public Guid Id { get; private set; }
        public Guid InvitadoId { get; private set; }
        public string TipoDocumentoCodigo { get; private set; }
        public string DocumentoIdentidad { get; private set; }
        public bool EsPrioridad { get; private set; }

        public InvitadoDocumento(
            Guid invitadoId,
            string tipoDocCodigo,
            string docIdentidad,
            bool esPrioridad)
        {
            Id = Guid.NewGuid();
            InvitadoId = invitadoId;
            TipoDocumentoCodigo = tipoDocCodigo;
            DocumentoIdentidad = docIdentidad;
            EsPrioridad = esPrioridad;
        }
        private InvitadoDocumento() { }
    }
}
