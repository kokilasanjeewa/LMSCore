using System.ComponentModel.DataAnnotations;

namespace Core.Application.Request;

public record LoginModel(string UserID,int ComSerialID, string Password);
public record LoginModelTenant(string UserID, string Password, string[] tenantName);
public record RefreshTokenModel(string token);

