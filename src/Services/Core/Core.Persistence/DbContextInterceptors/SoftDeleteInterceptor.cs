using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Core.Application.Interfaces;
using Core.Domain.Common;
using Core.Persistence.Contexts;

namespace Core.Persistence.DbContextInterceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            /* if (eventData.Context is null)
             {
                 return base.SavingChangesAsync(
                     eventData, result, cancellationToken);
             }*/
            if (eventData.Context is not DbContext context)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            if (context is ApplicationDbContext appContext && appContext.BypassSoftDelete)
            {
                // Bypassing soft delete — allow hard delete
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }
            IEnumerable<EntityEntry<ISoftDeletable>> entries =
                eventData
                    .Context
                    .ChangeTracker
                    .Entries<ISoftDeletable>()
                    .Where(e => e.State == EntityState.Deleted);

            foreach (EntityEntry<ISoftDeletable> softDeletable in entries)
            {
                switch (softDeletable)
                {
                    case { State: EntityState.Deleted, Entity: BaseAuditableEntity { IsDeleted: true } delete  }:
                        softDeletable.State = EntityState.Modified;
                        delete.IsDeleted = true;
                        delete.ModifiedDate = DateTime.Now;
                        break;
                    case { State: EntityState.Modified, Entity: BaseAuditableEntity { IsDeleted: false} update }:
                        update.IsDeleted = false;
                        update.ModifiedDate = DateTime.Now;
                        break;

                    default:
                        softDeletable.State = EntityState.Modified;
                        softDeletable.Entity.IsDeleted = true;
                        softDeletable.Entity.DeletedOnUtc = DateTime.Now;
                        break;
                }

            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
