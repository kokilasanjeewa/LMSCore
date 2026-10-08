using System.Text.Json.Serialization;

namespace Core.Application.Response;

public record AuthenticationToken(string Token, int ExpiresIn,string UserName, string UserID);




