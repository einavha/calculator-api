namespace SimpleCalculatorService.Models
{

    /// <summary>  
    /// Represents the result of a calculation operation.  
    /// </summary>  
    public class CalculationResult
    {
        /// <summary>  
        /// Gets or sets the result of the calculation.  
        /// </summary>  
        public decimal Result { get; set; }

        /// <summary>  
        /// Initializes a new instance of the <see cref="CalculationResult"/> class with a specified result.  
        /// </summary>  
        /// <param name="result">The result of the calculation.</param>  
        public CalculationResult(decimal result)
        {
            Result = result;
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="CalculationResult"/> class with a default result of 0.  
        /// </summary>  
        public CalculationResult()
        {
            Result = 0;
        }
    }


}