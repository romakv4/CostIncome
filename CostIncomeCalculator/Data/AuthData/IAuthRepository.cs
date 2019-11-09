using System.Threading.Tasks;
using CostIncomeCalculator.Models;
using Microsoft.AspNetCore.Mvc;

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
        /// <returns><see cref="User" /></returns>
        Task<User> Register(User user, string password);

        /// <summary>
        /// User login method. See implementation here <see cref="AuthRepository.Login" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">Password</param>
        /// <returns>If success login <see cref="User" />, else null</returns>
        Task<User> Login(string email, string password);

        /// <summary>
        /// Change password method. See implementation here <see cref="AuthRepository.ChangePassword" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="oldPassword">Old user password</param>
        /// <param name="newPassword">New user password</param>
        /// <returns>If success change <see cref="User" />, else null</returns>
        Task<User> ChangePassword(string email, string oldPassword, string newPassword);

        /// <summary>
        /// Reset password method. See implementation here <see cref="AuthRepository.ResetPassword" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>If success reset <see cref="User" />, else null</returns>
        Task<User> ResetPassword(string email);
    }
}