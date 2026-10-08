using Core.Application.Response;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface ITokenService
    {
        AuthenticationToken? GenerateAuthToken(User user);
        RefreshToken CreateRefreshToken();
    }
}
