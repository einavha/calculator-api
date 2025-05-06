using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using SimpleCalculatorService.Services;

namespace SimpleCalculatorService.Security
{

    /// <summary>
    /// Class to handle bearer authentication.
    /// </summary>
    public class BearerAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Constructor for BearerAuthenticationHandler.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="jwrService"></param>
        public BearerAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, IJwtService jwrService) 
            : base(options, logger, encoder)
        {
            _jwtService = jwrService;
        }

        /// <summary>
        /// Scheme name for authentication handler., 
        /// </summary>
        public const string SchemeName = "Bearer";

        /// <summary>
        /// Verifies that the required authorization header exists.
        /// </summary>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            }
            try
            {
                string? token = Request?.Headers?.Authorization;
                var authHeader = AuthenticationHeaderValue.Parse(token ?? "");
                var valid = _jwtService.ValidateToken(authHeader.Parameter ?? "", Scheme, out AuthenticationTicket? ticket);

                if (ticket is not null)
                {
                    return
                        Task.FromResult(valid ? AuthenticateResult.Success(ticket) 
                        : AuthenticateResult.Fail("Toke expired"));
                }
                return Task.FromResult(AuthenticateResult.Fail("Invalid Token"));
            }
            catch(Exception ex) 
            {
                return Task.FromResult(AuthenticateResult.Fail(ex.Message));
            }
        }
    }
}
