

using JwtTokenAuthentication.Dto;
using System.ComponentModel.DataAnnotations;

namespace JwtTokenAuthentication.Services
{
    public interface IJTokenService
    {
        bool IsTokenInvalidated(string token);
    }
}
