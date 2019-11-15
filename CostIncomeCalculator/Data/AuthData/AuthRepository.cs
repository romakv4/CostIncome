using System;
using System.Threading.Tasks;
using CostIncomeCalculator.Helpers;
using CostIncomeCalculator.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CostIncomeCalculator.CustomExceptions;

namespace CostIncomeCalculator.Data.AuthData
{
    /// <summary>
    /// Auth repository class.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        private readonly IPasswordHasher passwordHasher;
        private readonly IEmailHelper emailHelper;
        private readonly ILogger logger;

        /// <summary>
        /// AuthRepository constructor.
        /// </summary>
        /// <param name="context"><see cref="DataContext" /></param>
        /// <param name="passwordHasher"><see cref="PasswordHasher" /></param>
        /// <param name="emailHelper"><see cref="EmailHelper"/>.</param>
        /// <param name="logger">Serilog logger.</param>
        public AuthRepository(DataContext context, IPasswordHasher passwordHasher, IEmailHelper emailHelper, ILogger logger)
        {
            this.emailHelper = emailHelper;
            this.logger = logger;
            this.passwordHasher = passwordHasher;
            this.context = context;
        }

        /// <summary>
        /// User registration method.
        /// </summary>
        /// <param name="user"><see cref="User" /></param>
        /// <param name="password">User password.</param>
        /// <returns><see cref="User" /></returns>
        public async Task<User> Register(User user, string password)
        {
            try
            {
                string passwordHash;
                passwordHasher.CreatePasswordHash(password, out passwordHash);

                user.PasswordHash = passwordHash;

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// User login method.
        /// </summary>
        /// <param name="email">User email in database.</param>
        /// <param name="password">User password.</param>
        /// <returns>If success login <see cref="User" />, else null</returns>
        public async Task<User> Login(string email, string password)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                {
                    return null;
                }

                if (passwordHasher.VerifyPasswordHash(password, user.PasswordHash))
                {
                    return user;
                }
                return null;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Change password method.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="oldPassword">Old user password</param>
        /// <param name="newPassword">New user password</param>
        /// <returns>If success change <see cref="User" />, else null</returns>
        public async Task<User> ChangePassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                    return null;

                if (oldPassword == newPassword)
                    throw new EqualsPasswordsException("New password must not be equals old password");

                if (!passwordHasher.VerifyPasswordHash(oldPassword, user.PasswordHash))
                    return null;

                string passwordHash;
                passwordHasher.CreatePasswordHash(newPassword, out passwordHash);

                user.PasswordHash = passwordHash;

                context.Users.Update(user);
                await context.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Reset password method.
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>If success reset <see cref="User" />, else null</returns>
        public async Task<User> ResetPassword(string email)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                    return null;

                string[] guidParts = Guid.NewGuid().ToString().Split('-');
                string newPassword = guidParts[guidParts.Length - 1];

                string passwordHash;
                passwordHasher.CreatePasswordHash(newPassword, out passwordHash);

                user.PasswordHash = passwordHash;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                emailHelper.SendResetPasswordEmail(email, newPassword);

                return user;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }
    }
}