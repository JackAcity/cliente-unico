using Domain.Primitives;
using Domain.ValueObjects;
namespace Domain.Entities.Auditoria
{

    public class Etapa : AggregateRoot
    {
        public long Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Trazabilidad> Trazas { get; set; }
        public AuditInfo AuditInfo { get; set; }
    }
}
