using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.Auditoria
{
    public class Trazabilidad : AggregateRoot
    {
        public Guid Id { get; set; }       
        public short? SistemaId { get; set; }    
        public string TramaJson { get; set; }    
        public int? Accion { get; set; }        
        public long? EtapaId { get; set; }
        public Etapa Etapa { get; set; }
        public ICollection<AuditoriaIntegracion> Auditorias { get; set; }
         public AuditInfo AuditInfo { get; set; }
    }
}
