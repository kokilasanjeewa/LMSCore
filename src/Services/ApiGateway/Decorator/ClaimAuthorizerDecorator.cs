
//https://www.appsloveworld.com/asp/aspnetcore/17/ocelot-routeclaimsrequirement-does-not-recognize-my-claims-and-returns-403-forbid
using Ocelot.DownstreamRouteFinder.UrlMatcher;
using Ocelot.Responses;
using Ocelot.Values;
using System.Security.Claims;

namespace OcelotApiGateway.Decorator
{
    public class ClaimAuthorizerDecorator : Ocelot.Authorization.IClaimsAuthorizer
    {
        private readonly Ocelot.Authorization.ClaimsAuthorizer _authoriser;

        public ClaimAuthorizerDecorator(Ocelot.Authorization.ClaimsAuthorizer authoriser)
        {
            _authoriser = authoriser;
        }
        Response<bool> Ocelot.Authorization.IClaimsAuthorizer.Authorize(ClaimsPrincipal claimsPrincipal, Dictionary<string, string> routeClaimsRequirement, List<PlaceholderNameAndValue> urlPathPlaceholderNameAndValues)
        {
            var newRouteClaimsRequirement = new Dictionary<string, string>();
            foreach (var kvp in routeClaimsRequirement)
            {
                if (kvp.Key.StartsWith("http///"))
                {
                    var key = kvp.Key.Replace("http///", "http://");
                    newRouteClaimsRequirement.Add(key, kvp.Value);
                }
                else
                {
                    newRouteClaimsRequirement.Add(kvp.Key, kvp.Value);
                }
            }

            return _authoriser.Authorize(claimsPrincipal, newRouteClaimsRequirement, urlPathPlaceholderNameAndValues);
        }
    }
}
