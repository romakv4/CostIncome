using System.Threading.Tasks;

namespace cost_income_calculator.api.Helpers
{
    /// <summary>
    /// IUserHelper interface.
    /// </summary>
    public interface IUserHelper
    {
        /// <summary>
        /// User exists.
        /// </summary>
        /// <param name="username">string</param>
        /// <returns>True if user exists, else false.</returns>
        Task<bool> UserExists(string username);
    }
}