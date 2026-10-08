using Core.Application.Request;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserName(string loginName, CancellationToken cancellationToken);
        Task<bool> UserIDExistsAsync(string loginName, CancellationToken cancellationToken);
        Task<bool> UserIDIsLoggedAsync(string loginName, CancellationToken cancellationToken);
        Task<bool> UserTokenExistsAsync(string token, CancellationToken cancellationToken);
        Task<User> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken);
        Task<bool> IsValidUserID(long userSerialID,CancellationToken cancellationToken);
        Task<List<User>> GetUsersAsync(CancellationToken cancellationToken);
    }
}
