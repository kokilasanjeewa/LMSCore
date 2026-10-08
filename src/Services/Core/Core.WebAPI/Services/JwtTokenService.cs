using Core.Application.Interfaces.Repositories;
using Core.Application.Response;
using Core.Domain.Entities;
using Dapper;
using JwtTokenAuthentication.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.WebAPI.Services;

public class JwtTokenService : ITokenService
{
    private readonly string _connectionString;

    public JwtTokenService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultTokenConnection")
            ?? configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultTokenConnection' or 'DefaultConnection' is not configured.");
    }

    public AuthenticationToken? GenerateAuthToken(User user)
    {
        var permissionMenuIds = LoadPermissionMenuIds(user.UserSerialID, user.PermissionType, user.GrpSerialID);

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtExtensions.SecurityKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var loginTime = DateTime.Now;
        var expirationTimeStamp = JwtExtensions.GetExpiration(loginTime);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, user.UserID ?? string.Empty),
            new Claim("userID", user.UserID ?? string.Empty),
            new Claim("userSerialID", user.UserSerialID.ToString()),
            new Claim("loginLogSerialID", (user.LoginLogSerialID ?? 0).ToString()),
            new Claim("permissionType", user.PermissionType.ToString()),
           // new Claim("scope", string.Join(" ", permissionMenuIds)),      // Optionally include permissions as a custom claim if needed for quick access, but be cautious of token size
            new Claim("permissions", string.Join(" ", permissionMenuIds)),
            new Claim("group", user.GrpSerialID?.ToString() ?? string.Empty),
            new Claim("ipAddress", user.LastIp ?? string.Empty),
            new Claim("sessionId", user.LastSessionId ?? string.Empty),
            new Claim("logincomSerialID", (user.LoginCompanySerialID ?? 0).ToString())
        };

        var tokenOptions = new JwtSecurityToken(
            issuer: "https://localhost:5002",
            claims: claims,
            expires: expirationTimeStamp,
            signingCredentials: signingCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new AuthenticationToken(
            tokenString,
            (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds,
            user.UserName ?? string.Empty,
            user.UserID ?? string.Empty);
    }

    public RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.Now.AddMinutes(2),
                Created = DateTime.Now
            };
        }
    }

    private IReadOnlyList<int> LoadPermissionMenuIds(int userSerialID, PermissionType permissionType, int? grpSerialID)
    {
        var menuIds = new HashSet<int>();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        if (permissionType == PermissionType.Group && grpSerialID is > 0)
        {
            foreach (var id in QueryGroupMenuIds(connection, grpSerialID.Value))
            {
                menuIds.Add(id);
            }
        }

        if (permissionType == PermissionType.Individual || menuIds.Count == 0)
        {
            foreach (var id in QueryUserMenuIds(connection, userSerialID))
            {
                menuIds.Add(id);
            }
        }

        return menuIds.OrderBy(id => id).ToList();
    }

    private static IEnumerable<int> QueryUserMenuIds(SqlConnection connection, int userSerialID)
    {
        const string sql = @"
            SELECT DISTINCT m.[MnuID]
            FROM [CoreDB].[Core].[UserMenuPermission] AS um
            INNER JOIN [CoreDB].[Core].[Menu] AS m ON um.MnuID = m.MnuID
            WHERE um.IsDeleted = 0
              AND um.Active = 1
              AND um.UserSerialID = @UserSerialID";

        return connection.Query<int>(sql, new { UserSerialID = userSerialID }, commandTimeout: 120);
    }

    private static IEnumerable<int> QueryGroupMenuIds(SqlConnection connection, int grpSerialID)
    {
        const string sql = @"
            SELECT DISTINCT m.[MnuID]
            FROM [CoreDB].[Core].[GroupMenu] AS gm
            INNER JOIN [CoreDB].[Core].[Menu] AS m ON gm.MnuID = m.MnuID
            WHERE gm.IsDeleted = 0
              AND gm.Active = 1
              AND gm.GrpSerialID = @GrpSerialID";

        return connection.Query<int>(sql, new { GrpSerialID = grpSerialID }, commandTimeout: 120);
    }
}
