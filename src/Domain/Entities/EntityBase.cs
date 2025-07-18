using Domain.Common;
using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;

namespace Domain.Entities
{
    public abstract class EntityBase : AggregateRoot, IActivable, IAuditableEntity
    {
        public AuditInfo? AuditInfo { get;  set; }

        protected EntityBase()
        {
        }
        protected EntityBase(AuditInfo auditInfo)
        {
            AuditInfo = auditInfo;
        }

        public bool EstaActivo() => AuditInfo?.Activo ??false;

        public void SetAudit(AuditInfo auditInfo)
        {
            if (AuditInfo != null )
                throw new InvalidOperationException("AuditInfo ya ha sido establecido.");
            AuditInfo = auditInfo;
        }
      
    }
}