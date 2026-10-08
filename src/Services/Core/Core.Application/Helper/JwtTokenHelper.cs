using System.Globalization;
using System.IdentityModel.Tokens.Jwt;

namespace Core.Application.Helper
{
    public static class JwtTokenHelper
    {
        private static readonly string[] SupportedExpirationFormats =
        {
            "MM/dd/yyyy, hh:mm:ss tt",
            "M/d/yyyy, h:mm:ss tt",
            "MM/dd/yyyy HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss.fffZ",
            "o"
        };
        /// <summary>
        /// Extracts the expiration date and time from a JWT token.
        /// </summary>
        /// <param name="token">The JWT token as a string.</param>
        /// <returns>The expiration date and time if available; otherwise, null.</returns>
        public static DateTime? GetTokenExpiration(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Token cannot be null or empty.", nameof(token));

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // Check if the token is in a valid JWT format
            if (!jwtTokenHandler.CanReadToken(token))
                throw new ArgumentException("Invalid token format.");

            var jwtToken = jwtTokenHandler.ReadJwtToken(token);

            // Find the expiration claim ("exp") and parse it
            var expirationClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Exp);

            if (expirationClaim == null)
                return null;

            // Convert expiration time from Unix time to DateTime
            var expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expirationClaim.Value)).UtcDateTime;
            return expirationTime;
        }

        /// <summary>
        /// Resolves token expiration from a form value and/or JWT <c>exp</c> claim.
        /// </summary>
        public static bool TryResolveExpirationTime(string? token, string? expirationTimeValue, out DateTime expirationTime)
        {
            expirationTime = default;

            if (!string.IsNullOrWhiteSpace(expirationTimeValue) &&
                TryParseExpirationValue(expirationTimeValue, out expirationTime))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            try
            {
                var fromToken = GetTokenExpiration(token.Trim().Trim('"'));
                if (fromToken.HasValue)
                {
                    expirationTime = fromToken.Value;
                    return true;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }

            return false;
        }

        private static bool TryParseExpirationValue(string value, out DateTime expirationTime)
        {
            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out expirationTime))
            {
                return true;
            }

            if (DateTime.TryParse(value, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out expirationTime))
            {
                return true;
            }

            if (DateTime.TryParseExact(
                    value,
                    SupportedExpirationFormats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out expirationTime))
            {
                return true;
            }

            return DateTime.TryParseExact(
                value,
                SupportedExpirationFormats,
                CultureInfo.GetCultureInfo("en-US"),
                DateTimeStyles.None,
                out expirationTime);
        }
    }
}