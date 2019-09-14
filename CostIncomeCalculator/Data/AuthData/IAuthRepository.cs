using System.Threading.Tasks;
using CostIncomeCalculator.Models;

namespace CostIncomeCalculator.Data.AuthData
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
    }
}