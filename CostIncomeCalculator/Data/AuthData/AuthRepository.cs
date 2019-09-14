using System.Threading.Tasks;
using CostIncomeCalculator.Helpers;
using CostIncomeCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace CostIncomeCalculator.Data.AuthData
{
    /// <summary>
    /// Auth repository class.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        private readonly IPasswordHasher passwordHasher;

        /// <summary>
        /// AuthRepository constructor.
        /// </summary>
        /// <param name="context"><see cref="DataContext" /></param>
        /// <param name="passwordHasher"><see cref="PasswordHasher" /></param>
        public AuthRepository(DataContext context, IPasswordHasher passwordHasher)
        {
            this.passwordHasher = passwordHasher;
            this.context = context;
        }

        /// <summary>
        /// User login method.
        /// </summary>
        /// <param name="username">Username in database.</param>
        /// <param name="password">User password.</param>
        /// <returns>If success login <see cref="User" />, else null</returns>
        public async Task<User> Login(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
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

        /// <summary>
        /// User registration method.
        /// </summary>
        /// <param name="user"><see cref="User" /></param>
        /// <param name="password">User password.</param>
        /// <returns><see cref="User" /></returns>
        public async Task<User> Register(User user, string password)
        {
            string passwordHash;
            passwordHasher.CreatePasswordHash(password, out passwordHash);

            user.PasswordHash = passwordHash;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}