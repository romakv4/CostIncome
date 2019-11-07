using System;
using System.Threading.Tasks;
using CostIncomeCalculator.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CostIncomeCalculator.Helpers
{
    /// <summary>
    /// UserHelper class.
    /// Contains methods to simply work with user.
    /// </summary>
    public class UserHelper : IUserHelper
    {
        private readonly DataContext context;

        /// <summary>
        /// UserHelper constructor.
        /// </summary>
        /// <param name="context">DataContext</param>
        public UserHelper(DataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Checks if the user exists in the database.
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>True if user exists, else false.</returns>
        public async Task<bool> UserExists(string email)
        {
            try
            {
                return await context.Users.AnyAsync(x => x.Email == email.ToLower());
            }
            catch
            {
                throw;
            }
        }
    }
}