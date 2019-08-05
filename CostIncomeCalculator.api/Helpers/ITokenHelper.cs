using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace cost_income_calculator.api.Helpers
{
    public interface ITokenHelper
    {
        string GenerateToken(User user, IConfiguration config);
        
        string GetUsername(HttpContext context);
    }
}