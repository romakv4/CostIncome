using System.Threading.Tasks;
using cost_income_calculator.api.Models;
using Microsoft.EntityFrameworkCore;

namespace cost_income_calculator.api.Data
{
    public class CostRepository : ICostRepository
    {
        private readonly DataContext context;
        public CostRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Cost> SetCost(string username, string type, string description, double price)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;
            
            var cost = new Cost
            {
                UserId = user.Id,
                Type = type,
                Description = description,
                Price = price
            };

            await context.AddAsync(cost);
            await context.SaveChangesAsync();

            return cost;
        }

        public async Task<Cost> EditCost(string username, int costId, string newType, string newDescription, double newPrice)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;
            
            var currentCost = await context.Costs.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
            if (currentCost == null) return null;

            currentCost.Type = newType;
            currentCost.Description = newDescription;
            currentCost.Price = newPrice;

            context.Costs.Update(currentCost);
            await context.SaveChangesAsync();

            return currentCost;
        }

        public async Task<Cost> DeleteCost(string username, int costId)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;
            
            var currentCost = await context.Costs.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
            if (currentCost == null) return null;

            context.Costs.Remove(currentCost);
            await context.SaveChangesAsync();
            
            return currentCost;
        }
    }
}