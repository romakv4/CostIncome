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
        private readonly ILogger logger;

        /// <summary>
        /// UserHelper constructor.
        /// </summary>
        /// <param name="context">DataContext</param>
        /// <param name="logger">ILogger</param>
        public UserHelper(DataContext context, ILogger logger)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <summary>
        /// Checks if the user exists in the database.
        /// </summary>
        /// <param name="username">string</param>
        /// <returns>True if user exists, else false.</returns>
        public async Task<bool> UserExists(string username)
        {
            try
            {
                return await context.Users.AnyAsync(x => x.Username == username.ToLower());
            }
            catch (Exception error)
            {
                logger.Error(error.Message);

                return false;
            }
        }
    }
}