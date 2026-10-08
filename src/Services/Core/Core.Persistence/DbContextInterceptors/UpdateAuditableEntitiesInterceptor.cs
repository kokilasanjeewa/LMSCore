using Core.Domain.Common.interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Core.Persistence.Contexts;

namespace Core.Persistence.DbContextInterceptors
{
    public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateAuditableEntitiesInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor != null)
            {
                _httpContextAccessor = httpContextAccessor;
            }
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            int userID = 0;
            var token = _httpContextAccessor?.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var applicationUserId = jwt.Claims.First(c => c.Type =="userSerialID");
                userID = applicationUserId.Value == null ? 0 : Convert.ToInt32(applicationUserId.Value);
            }

            var dbContext = eventData.Context;

            if (dbContext is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            // track auditable changes Added / Modified / Deleted  states 
            var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>();

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        // When a new entity is added, set its creation audit properties.
                        entityEntry.Property(o => o.CreatedDate).CurrentValue = DateTime.Now;
                        entityEntry.Property(o => o.CreatedBy).CurrentValue = userID;
                        break;

                    case EntityState.Modified when entityEntry.Property(o => o.IsDeleted).CurrentValue &&
                                                   !entityEntry.Property(o => o.IsDeleted).OriginalValue:
                        // If the entity is being soft-deleted (changed from false to true),
                        // update the modified audit properties.
                        entityEntry.Property(o => o.ModifiedDate).CurrentValue = DateTime.Now;
                        entityEntry.Property(o => o.ModifiedBy).CurrentValue = userID;
                        // Optionally, if you are performing a soft delete, set the state to Modified 
                        // so that EF Core will perform an update rather than a delete.
                        entityEntry.State = EntityState.Modified;
                        break;

                    case EntityState.Modified:
                        // For general modifications, update the modified audit properties.
                        entityEntry.Property(o => o.ModifiedDate).CurrentValue = DateTime.Now;
                        entityEntry.Property(o => o.ModifiedBy).CurrentValue = userID;
                        break;

                    case EntityState.Deleted:
                        if (dbContext is ApplicationDbContext ctx && ctx.BypassSoftDelete)
                        {
                            // Allow hard delete, do not change state or mark as IsDeleted
                            entityEntry.State = EntityState.Deleted;
                            break;
                        }
                        // For deletes, if you are implementing a soft delete:
                        entityEntry.Property(o => o.ModifiedDate).CurrentValue = DateTime.Now;
                        entityEntry.Property(o => o.ModifiedBy).CurrentValue = userID;
                        entityEntry.Property(o => o.IsDeleted).CurrentValue = true;
                        // Instead of keeping the Deleted state, mark it as Modified so the soft delete gets saved.
                        entityEntry.State = EntityState.Modified;
                        break;

                    case EntityState.Detached:
                        // Do nothing if the entity is not tracked.
                        break;

                    case EntityState.Unchanged:
                        // No changes; nothing to do.
                        break;

                    default:
                        // Default action if an unhandled state is encountered.
                        break;
                }
            }


            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
