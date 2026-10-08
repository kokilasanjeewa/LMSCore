
namespace JwtTokenAuthentication.Services;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using JwtTokenAuthentication.Extensions;
using Microsoft.AspNetCore.Http;

public class TokenService
{
    private const int ExpirationMinutes = 30;
    private readonly ILogger<TokenService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(ILogger<TokenService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public string[]? ValidateJwtToken(string token)
    {
        if (token == null)
            return null;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(JwtExtensions.SecurityKey);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,

                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero

            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var scope = jwtToken.Claims.First(x => x.Type.Contains("permissions")).Value; //scope change to permissions by kokila 9/4/2025
            string[] scopeArray = new string[] { "" };

            //somewhere in your code
            scopeArray = scope.Split(' ');

            // return user id from JWT token if validation successful
            return scopeArray;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Token validation failed: {ex.Message}");

            // return null if validation fails
            return null;
        }
    }
    public int GetUserSerialID()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("Authorization token is missing or invalid.");

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var userSerialIDClaim = jwt.Claims.FirstOrDefault(c => c.Type == "userSerialID");

        if (userSerialIDClaim == null)
            throw new UnauthorizedAccessException("userSerialID claim is missing in the token.");

        return Convert.ToInt32(userSerialIDClaim.Value);
    }
}