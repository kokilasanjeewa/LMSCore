using JwtTokenAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace JwtTokenAuthentication.Permission
{
    public class MultiplePermissionAuthorizationHandler : AuthorizationHandler<MultiplePermissionRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenService _tokenService;

        public MultiplePermissionAuthorizationHandler(IHttpContextAccessor httpContextAccessor, TokenService tokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MultiplePermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();

            HashSet<string>? permissionIds = null;
            if (token != null)
            {
                var scope = _tokenService.ValidateJwtToken(token);
                if (scope != null)
                {
                    permissionIds = scope
                        .Where(id => !string.IsNullOrWhiteSpace(id))
                        .ToHashSet(StringComparer.Ordinal);
                }
            }

            if (permissionIds != null &&
                requirement.PermissionIds.Any(id => permissionIds.Contains(id.ToString())))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
