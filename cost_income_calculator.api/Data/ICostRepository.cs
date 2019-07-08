using System.Threading.Tasks;
using cost_income_calculator.api.Models;

namespace cost_income_calculator.api.Data
{
    public interface ICostRepository
    {
        Task<Cost> SetCost(string username, string type, string description, double price);
        Task<Cost> EditCost(string username, int costId, string newType, string newDescription, double newPrice);
        Task<Cost> DeleteCost(string username, int costId);
    }
}