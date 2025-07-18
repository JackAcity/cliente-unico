using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.Auditoria
{
    public class AuditoriaIntegracion : AggregateRoot
    {
        public Guid Id { get; set; }     
        public long SistemaId { get; set; }       
        public string Log { get; set; }           
        public Guid? TrazabilidadId { get; set; }  
        public string Ubicacion { get; set; }    
        public string SistemaOrigenInvitado { get; set; } 

        public Trazabilidad Trazabilidad { get; set; }
         public AuditInfo AuditInfo { get; set; }
    }
}
