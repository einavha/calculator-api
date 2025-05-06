using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCalculatorService.Models;
using SimpleCalculatorService.Services;

namespace SimpleCalculatorService.Controllers
{
    /// <summary>
    /// AuthorizationController
    /// </summary>
    public class AuthorizationController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthorizationController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jwtService"></param>
        /// <param name="logger"></param>
        public AuthorizationController(IJwtService jwtService, ILogger<AuthorizationController> logger)
        {
            _jwtService = jwtService;
            _logger = logger;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/v1/login")]
        [ProducesResponseType(typeof(InlineResponse200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(InlineServerErrorResponse500), StatusCodes.Status500InternalServerError)]
        public string ApiLogin([FromBody] string username)
        {
            _logger.LogInformation($"Login attempt for username: {username}");

            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("Invalid username provided");
                    return "Invalid username";
                }

                var token = _jwtService.GenerateToken(username);
                _logger.LogInformation($"Token generated for user: {username}");
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during login for user: {username}");
                return $"Error: {ex.Message}";
            }
        }
    }
}