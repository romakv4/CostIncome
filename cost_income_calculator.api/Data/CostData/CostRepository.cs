using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cost_income_calculator.api.Dtos.CostDtos;
using cost_income_calculator.api.Helpers;
using cost_income_calculator.api.Models;
using Microsoft.EntityFrameworkCore;

namespace cost_income_calculator.api.Data.CostData
{
    public class CostRepository : ICostRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IDatesHelper datesHelper;
        public CostRepository(DataContext context, IMapper mapper, IDatesHelper datesHelper)
        {
            this.datesHelper = datesHelper;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<CostReturnDto>> GetAllCosts(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);

            List<Cost> costs = new List<Cost>();

            costs = await context.Costs.ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(costs);
        }

        public async Task<IEnumerable<CostReturnDto>> GetMonthlyCosts(string username, DateTime currentDate)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            
            (DateTime, DateTime) dates = datesHelper.GetFirstAndLastDateOfMonth(currentDate);
            var monthlyCosts = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(monthlyCosts);
        }

        public async Task<Cost> SetCost(string username, string type, string description, double price, DateTime date)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);

            var cost = new Cost
            {
                UserId = user.Id,
                Type = type,
                Description = description,
                Price = price,
                Date = date
            };

            await context.AddAsync(cost);
            await context.SaveChangesAsync();

            return cost;
        }

        public async Task<Cost> EditCost(string username, int costId, string newType, string newDescription, double newPrice, DateTime newDate)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);

            var currentCost = await context.Costs.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
            if (currentCost == null) return null;

            currentCost.Type = newType;
            currentCost.Description = newDescription;
            currentCost.Price = newPrice;

            context.Costs.Update(currentCost);
            await context.SaveChangesAsync();

            return currentCost;
        }

        public async Task<List<Cost>> DeleteCosts(string username, int[] costIds)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);

            List<Cost> costs = new List<Cost>();

            foreach (var costId in costIds)
            {
                var costForDelete = await context.Costs.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
                if (costForDelete == null) return null;
                context.Costs.Remove(costForDelete);
                costs.Add(costForDelete);
            }

            await context.SaveChangesAsync();

            return costs;
        }
    }
}