using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cost_income_calculator.api.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        public string GetUsername(HttpContext context)
        {
            return context.User.Identity.Name;
        }
    }
}