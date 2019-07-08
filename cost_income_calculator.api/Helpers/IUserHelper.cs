using System.Threading.Tasks;

namespace cost_income_calculator.api.Helpers
{
    public interface IUserHelper
    {
        Task<bool> UserExists(string username);
    }
}