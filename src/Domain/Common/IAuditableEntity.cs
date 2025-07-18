using Domain.ValueObjects;

namespace Domain.Common
{
    public interface IAuditableEntity
    {
        void SetAudit(AuditInfo auditInfo);
    }

}
