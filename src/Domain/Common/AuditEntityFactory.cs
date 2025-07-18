using Domain.ValueObjects;

namespace Domain.Common
{
    public static class AuditEntityFactory
    {
        public static T Crear<T>(Func<AuditInfo, T> entityBuilder, string usuario)
              where T : IAuditableEntity
        {
            var audit = AuditInfo.Create(usuario) ?? throw new InvalidOperationException("AuditInfo no se pudo crear.");
            var entity = entityBuilder(audit);
            return entity;
        }
    }
}
