using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SimpleCalculatorService.Attributes;
using SimpleCalculatorService.Models;
using SimpleCalculatorService.Security;
using SimpleCalculatorService.Services;

namespace SimpleCalculatorService.Controllers
{
    /// <summary>
    /// Controller for performing calculator operations.
    /// </summary>
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        private readonly ILogger<CalculatorController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorController"/> class.
        /// </summary>
        /// <param name="calculatorService">The calculator service to perform operations.</param>
        /// <param name="logger">The logger instance for this class.</param>
        public CalculatorController(ICalculatorService calculatorService, ILogger<CalculatorController> logger)
        {
            _calculatorService = calculatorService;
            _logger = logger;
        }

        /// <summary>
        /// Performs a calculation based on the provided request and operation.
        /// </summary>
        /// <param name="body">The request body containing the numbers.</param>
        /// <param name="xOperation">The operation to perform.</param>
        /// <response code="200">Calculation successful</response>
        /// <response code="400">Invalid input</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Server Error</response>
        [HttpPost]
        [Route("/api/v1/calculate")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [ProducesResponseType(typeof(InlineResponse200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(InlineServerErrorResponse500), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InlineUnAuthResponse401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(InlineServerErrorResponse500), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CalculateBody body, [Required()][FromHeader] ICalculatorService.CalculatorOperation xOperation)
        {
            _logger.LogInformation($"Calculation request received. Operation: {xOperation}, Body: {System.Text.Json.JsonSerializer.Serialize(body)}");

            if (body is null)
            {
                _logger.LogWarning("Request body is null");
                return BadRequest("Request body is required");
            }

            if (body.Number1 is null || body.Number2 is null)
            {
                _logger.LogWarning("Number1 or Number2 is null");
                return BadRequest("Number1 or Number2 cannot be null");
            }

            try
            {
                var result = _calculatorService.Calculate(body.Number1 ?? 0, body.Number2 ?? 0, xOperation!);
                _logger.LogInformation($"Calculation successful. Result: {result}");
                return Ok(new CalculationResult(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during calculation");
                return BadRequest(ex.Message);
            }
        }
    }
}