using Core.Application.Features.Users.Queries.GetTokenByLogin;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using Core.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IGenericRepository<InvalidateToken> _repositoryToken;

        public UserRepository(IGenericRepository<User> repository, IGenericRepository<InvalidateToken> repositoryToken) 
        {
            _repository = repository;
            _repositoryToken= repositoryToken;


        }
         public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _repository.Entities.ToListAsync(cancellationToken);
        }
        public async Task<User> GetUserByUserName(string loginName, CancellationToken cancellationToken)
        {
            return await _repository.Entities.Where(x => x.UserID == loginName).Include(x => x.RefreshTokens).Include(x => x.Companies).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        }
        public async Task<bool> UserIDExistsAsync(string loginName, CancellationToken cancellationToken)
        {
            return await _repository.Entities.AnyAsync(x => x.UserID == loginName, cancellationToken);
        }
        public async Task<bool> UserIDIsLoggedAsync(string loginName, CancellationToken cancellationToken)
        {
            return await _repository.Entities.AnyAsync(x => x.UserID == loginName && x.IsLogOut==true, cancellationToken);
        }
        public async Task<bool> UserTokenExistsAsync(string token, CancellationToken cancellationToken)
        {
            return await _repositoryToken.Entities.AnyAsync(x => x.Token == token,cancellationToken);
        }
        public async Task<User> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            return await _repository.Entities.Where(u => u.RefreshTokens.Any(t => t.Token == refreshToken)).Include(x => x.RefreshTokens).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        }
        public async Task<bool> IsValidUserID(long userSerialID, CancellationToken cancellationToken)
        {
            return await _repository.Entities.Where(u => u.UserSerialID== userSerialID).AnyAsync(cancellationToken: cancellationToken);
        }
    }
}
