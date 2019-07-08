using System.Threading.Tasks;

namespace cost_income_calculator.api.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
    }
}