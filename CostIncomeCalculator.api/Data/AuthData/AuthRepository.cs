using System.Threading.Tasks;
using cost_income_calculator.api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace cost_income_calculator.api.Data.AuthData
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        private readonly IPasswordHasher passwordHasher;
        public AuthRepository(DataContext context, IPasswordHasher passwordHasher)
        {
            this.passwordHasher = passwordHasher;
            this.context = context;
        }

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