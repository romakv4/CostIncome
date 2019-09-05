using System.Threading.Tasks;

namespace CostIncomeCalculator.Helpers
{
    /// <summary>
    /// IUserHelper interface.
    /// </summary>
    public interface IUserHelper
    {
        /// <summary>
        /// Checks if the user exists in the database.
        /// </summary>
        /// <param name="username">string</param>
        /// <returns>True if user exists, else false.</returns>
        Task<bool> UserExists(string username);
    }
}