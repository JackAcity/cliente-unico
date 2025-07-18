using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _userSvc;
        private readonly IPublisher _publisher;

        public AuditInterceptor(ICurrentUserService userSvc)
           => _userSvc = userSvc;

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            ApplyAudit(eventData.Context!);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken ct = default)
        {
            ApplyAudit(eventData.Context!);
            return base.SavingChangesAsync(eventData, result, ct);
        }

        private void ApplyAudit(DbContext ctx)
        {
            var user = _userSvc.UserName;
            foreach (var entry in ctx.ChangeTracker
                                     .Entries<EntityBase>()
                                     .Where(e => e.State == EntityState.Added
                                              || e.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.SetAudit(AuditInfo.Create(user));
                else
                {
                    var aud = entry.Entity.AuditInfo!;
                    aud.Update(user);
                    entry.Entity.SetAudit(aud);
                }
            }
        }
    }
}
