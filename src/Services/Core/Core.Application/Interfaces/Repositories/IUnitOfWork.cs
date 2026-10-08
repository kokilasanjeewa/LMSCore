using Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;

        Task<int> Save(CancellationToken cancellationToken);

        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

        Task Rollback();
        //new test code
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> SaveNoCommitRoll(CancellationToken cancellationToken);

        Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken = default);

    }
}
