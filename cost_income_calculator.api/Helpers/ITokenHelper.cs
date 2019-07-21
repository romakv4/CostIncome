using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cost_income_calculator.api.Helpers
{
    public interface ITokenHelper
    {
        string GetUsername(HttpContext context);
    }
}