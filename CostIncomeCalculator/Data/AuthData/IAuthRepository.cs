using System.Threading.Tasks;
using CostIncomeCalculator.Models;

namespace CostIncomeCalculator.Data.AuthData
{
    /// <summary>
    /// Auth repository interface.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// User registration method. See iplementation here <see cref="AuthRepository.Register" />.
        /// </summary>
        /// <param name="user">User object for write to database.</param>
        /// <param name="password">Password.</param>
        /// <returns><see cref="CostIncomeCalculator.Models.User" /></returns>
         Task<User> Register(User user, string password);

         /// <summary>
         /// User login method. See implementation here <see cref="AuthRepository.Login" />.
         /// </summary>
         /// <param name="username">Username</param>
         /// <param name="password">Password</param>
         /// <returns>If success login <see cref="CostIncomeCalculator.Models.User" />, else null</returns>
         Task<User> Login(string username, string password);
    }
}