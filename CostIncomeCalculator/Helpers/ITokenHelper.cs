using CostIncomeCalculator.Models;

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
        /// <param name="secret">string</param>
        /// <returns>Token</returns>
        string GenerateToken(User user, string secret);
    }
}