using Microsoft.Extensions.DependencyInjection;
using SimpleCalculatorService.Services;
using static SimpleCalculatorService.Services.ICalculatorService;

namespace SimpleCalculatorServiceTests
{
    public class MyTestFixture
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public MyTestFixture()
        {
            var services = new ServiceCollection();
            services.AddTransient<ICalculatorService, CalculatorService>(); 

            ServiceProvider = services.BuildServiceProvider();
        }
    }

    public class CalculatorServiceTest: IClassFixture<MyTestFixture>
    {
        private readonly ICalculatorService? _service;

        public CalculatorServiceTest(MyTestFixture fixture)
        {
            _service = fixture?.ServiceProvider?.GetService<ICalculatorService>();
        }

        [Theory]
        [InlineData(2, 3, CalculatorOperation.Add, 5)]
        [InlineData(5, 2, CalculatorOperation.Subtract, 3)]
        [InlineData(3, 4, CalculatorOperation.Multiply, 12)]
        [InlineData(10, 2, CalculatorOperation.Divide, 5)]

        public void Calculate_ValidOperations_ReturnsExpectedResult(decimal n1, decimal n2, CalculatorOperation op, decimal expected)
        {
            var result = _service?.Calculate(n1, n2, op);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculate_DivideByZero_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _service?.Calculate(5, 0, CalculatorOperation.Divide));
        }
    }
}