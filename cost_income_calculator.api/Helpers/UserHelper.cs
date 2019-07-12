using System;
using System.Threading.Tasks;
using cost_income_calculator.api.Data;
using Microsoft.EntityFrameworkCore;

namespace cost_income_calculator.api.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext context;
        public UserHelper(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                if (await context.Users.AnyAsync(x => x.Username == username.ToLower()))
                    return true;

                return false;
            }
            catch (Exception error)
            {
                if (error is ArgumentNullException || error is InvalidOperationException)
                    Console.WriteLine(error); // Log to file here
                
                return false;
            }
        }
    }
}