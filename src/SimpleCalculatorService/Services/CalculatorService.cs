using Microsoft.Extensions.Logging;
using static SimpleCalculatorService.Services.ICalculatorService;

namespace SimpleCalculatorService.Services
{
    /// <summary>
    /// Provides methods to perform basic arithmetic operations.
    /// </summary>
    public class CalculatorService : ICalculatorService
    {
        private readonly ILogger<CalculatorService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for this class.</param>
        public CalculatorService(ILogger<CalculatorService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Performs a calculation based on the specified operation.
        /// </summary>
        /// <param name="num1">The first number.</param>
        /// <param name="num2">The second number.</param>
        /// <param name="operation">The operation to perform. Supported values are "add", "subtract", "multiply", and "divide".</param>
        /// <returns>The result of the calculation.</returns>
        /// <exception cref="ArgumentException">Thrown when an invalid operation is specified or when attempting to divide by zero.</exception>
        public decimal Calculate(decimal num1, decimal num2, CalculatorOperation operation)
        {
            _logger.LogInformation($"Performing {operation} with {num1} and {num2}");

            try
            {
                decimal result = operation switch
                {
                    CalculatorOperation.Add => num1 + num2,
                    CalculatorOperation.Subtract => num1 - num2,
                    CalculatorOperation.Multiply => num1 * num2,
                    CalculatorOperation.Divide => num2 != 0 ? num1 / num2 : throw new ArgumentException("Cannot divide by zero"),
                    _ => throw new ArgumentException("Invalid operation")
                };

                _logger.LogInformation($"Calculation result: {result}");
                return result;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error during calculation");
                throw; // Re-throw the exception to be handled by the caller
            }
        }
    }
}