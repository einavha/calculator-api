namespace SimpleCalculatorService.Services
{

    /// <summary>  
    /// Provides methods to perform basic calculations.  
    /// </summary>  
    public interface ICalculatorService
    {
        /// <summary>
        /// Represents the supported calculator operations.
        /// </summary>
        public enum CalculatorOperation
        {
            /// <summary>
            /// Represents the addition operation.
            /// </summary>
            Add,

            /// <summary>
            /// Represents the subtraction operation.
            /// </summary>
            Subtract,

            /// <summary>
            /// Represents the multiplication operation.
            /// </summary>
            Multiply,

            /// <summary>
            /// Represents the division operation.
            /// </summary>
            Divide
        }

        /// <summary>  
        /// Performs a calculation based on the specified operation.  
        /// </summary>  
        /// <param name="num1">The first number.</param>  
        /// <param name="num2">The second number.</param>  
        /// <param name="operation">The operation to perform (e.g., "add", "subtract").</param>  
        /// <returns>The result of the calculation.</returns>  
        decimal Calculate(decimal num1, decimal num2, CalculatorOperation operation);
    }
}