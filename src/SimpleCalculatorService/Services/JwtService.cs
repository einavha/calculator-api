using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace SimpleCalculatorService.Services
{
    /// <summary>
    /// Service for generating JSON Web Tokens (JWT).
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="logger">The logger instance for this class.</param>
        public JwtService(IConfiguration configuration, ILogger<JwtService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Validates the specified token using the provided authentication scheme.
        /// </summary>
        /// <param name="token">The token to validate.</param>
        /// <param name="scheme">The authentication scheme.</param>
        /// <param name="validatedTicket">The validated authentication ticket (output).</param>
        /// <returns>True if the token is valid, false otherwise.</returns>
        public bool ValidateToken(string token, AuthenticationScheme scheme, out AuthenticationTicket? validatedTicket)
        {
            _logger.LogInformation($"Validating token: {token} with scheme: {scheme.Name}");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration[Consts.JwtKey]!);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                validatedTicket = new AuthenticationTicket(principal, scheme.Name);

                bool isValid = jwtToken.ValidTo > DateTime.UtcNow;
                _logger.LogInformation($"Token validation result: {isValid}");
                return isValid;
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogError(ex, "Token validation failed");
                validatedTicket = null;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during token validation");
                validatedTicket = null;
                return false;
            }
        }

        /// <summary>
        /// Generates a JWT for the specified user.
        /// </summary>
        /// <param name="username">The user for whom the token is generated.</param>
        /// <returns>A JWT as a string.</returns>
        public string GenerateToken(string username)
        {
            _logger.LogInformation($"Generating token for user: {username}");

            var DurationInMinutes = 30;

            var tmpDuration = _configuration[Consts.JwtDurationInMinutes];
            if (!string.IsNullOrWhiteSpace(tmpDuration))
            {
                int.TryParse(tmpDuration, out DurationInMinutes);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration[Consts.JwtKey]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim(ClaimTypes.Name, username)
               }),
                Expires = DateTime.UtcNow.AddMinutes(DurationInMinutes), // claim expiration
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var generatedToken = tokenHandler.WriteToken(token);

                _logger.LogInformation($"Token generated successfully: {generatedToken}");
                return generatedToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating token");
                throw; 
            }
        }
    }
}