using Microsoft.AspNetCore.Authentication;

namespace SimpleCalculatorService.Services
{
    /// <summary>
    /// Provides methods for generating JSON Web Tokens (JWT) for a user.
    /// </summary>
    public interface IJwtService
    {
        string GenerateToken(string username);
        bool ValidateToken(string token, AuthenticationScheme scheme, out AuthenticationTicket? validatedTicket);
    }
}