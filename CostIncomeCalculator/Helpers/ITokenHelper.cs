using CostIncomeCalculator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CostIncomeCalculator.Helpers
{   
    /// <summary>
    /// ITokenHelper interface.
    /// </summary>
    public interface ITokenHelper
    {
        /// <summary>
        /// Generate token. See implementation here <see cref="TokenHelper.GenerateToken" />.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="config">IConfiguration</param>
        /// <returns>Token</returns>
        string GenerateToken(User user, IConfiguration config);
        
        /// <summary>
        /// Get username from HttpContext. See implementation here <see cref="TokenHelper.GetUsername" />.
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>Username</returns>
        string GetUsername(HttpContext context);
    }
}