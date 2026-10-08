using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace JwtTokenAuthentication.Permission
{

    public class CustomAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _fallbackPolicyProvider;

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => _fallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => _fallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var permissionIds = policyName.Split("||")
                                          .Select(id => int.TryParse(id.Trim(), out int parsedId) ? parsedId : (int?)null)
                                          .Where(id => id.HasValue)
                                          .Select(id => id.Value)
                                          .ToArray();
            if (permissionIds.Any())
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new MultiplePermissionRequirement(permissionIds));
                return Task.FromResult(policy.Build());
            }

            return _fallbackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
