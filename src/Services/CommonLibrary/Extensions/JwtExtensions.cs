using JwtTokenAuthentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtTokenAuthentication.Extensions
{
    public static class JwtExtensions
    {
        public const string SecurityKey = "secretJWTsigningKey@123 this is dptney core 8 security token with 70 charaters";
        public const string Issuer="SecureApi";
        public const string Audience="SecureApiUser";
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddTransient<IJTokenService, JTokenService>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:5002",
                    ValidateAudience = false,
                    ValidAudiences = new[] { "https://localhost:5005", "https://localhost:5006","https://localhost:5002", "https://localhost:5289" },
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var token = context.SecurityToken;

                        if (token != null)
                        {
                                var tokenService = context.HttpContext.RequestServices.GetRequiredService<IJTokenService>();

                            if (tokenService.IsTokenInvalidated(token.ToString()))
                            {
                                context.Fail("This token has been revoked.");
                            }
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
        public static DateTime GetExpiration(DateTime loginTime)
        {
            var hour = loginTime.Hour;

            if (hour >= 7 && hour < 10)
            {
                // Rule 1: 7am - 10am → +8 hours
                return loginTime.AddHours(8);
            }
            else if (hour >= 10 && hour < 17)
            {
                // Rule 2: 10am - 5pm → until 6pm
                var sixPm = loginTime.Date.AddHours(18); // 6pm today
                return sixPm;
            }
            else
            {
                // Rule 3: 6pm - 11.59pm OR 12am - 7am → +1 hour
                return loginTime.AddHours(1);
            }
        }
    }
}
