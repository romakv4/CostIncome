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
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

            List<Cost> costs = new List<Cost>();

            costs = await context.Costs.ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(costs);
        }

        public async Task<IEnumerable<CostReturnDto>> GetWeeklyCosts(string username, DateTime date)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetWeekDateRange(date);
            var weeklyCosts = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(weeklyCosts);
        }

        public async Task<IEnumerable<CostReturnDto>> GetWeeklyCostsByCategory(string username, DateTime date, string category)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetWeekDateRange(date);
            var weeklyCostsByCategory = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).Where(x => x.Type == category.ToLower()).ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(weeklyCostsByCategory);
        }

        public async Task<IEnumerable<CostReturnDto>> GetMonthlyCosts(string username, DateTime date)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(date);
            var monthlyCosts = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(monthlyCosts);
        }

        public async Task<IEnumerable<CostReturnDto>> GetMonthlyCostsByCategory(string username, DateTime date, string category)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(date);
            var monthlyCostsByCategory = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).Where(x => x.Type == category.ToLower()).ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(monthlyCostsByCategory);
        }

        public async Task<MonthCostDto> GetMaxCostsCategoryInMonth(string username, DateTime date)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(date);
            var monthlyCosts = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();
            var categories = monthlyCosts.Select(x => x.Type).Distinct();
            
            List<MonthCostDto> costs = new List<MonthCostDto>();
            foreach (var category in categories)
            {
                costs.Add(new MonthCostDto { 
                    Type = category.ToLower(), 
                    CostSum = monthlyCosts.Where(x => x.Type == category.ToLower()).Select(x => x.Price).Sum() 
                    });
            }

            return costs.FirstOrDefault(x => x.CostSum == costs.Max(z => z.CostSum));
        }

        public async Task<Cost> SetCost(string username, string type, string description, double price, DateTime date)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

            var cost = new Cost
            {
                UserId = user.Id,
                Type = type.ToLower(),
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
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

            var currentCost = await context.Costs.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
            if (currentCost == null) return null;

            currentCost.Type = newType.ToLower();
            currentCost.Description = newDescription;
            currentCost.Price = newPrice;

            context.Costs.Update(currentCost);
            await context.SaveChangesAsync();

            return currentCost;
        }

        public async Task<List<Cost>> DeleteCosts(string username, int[] costIds)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

            List<Cost> costs = new List<Cost>();

            foreach (var costId in costIds)
            {
                var costForDelete = await context.Costs.Where(x => x.user.Username == username.ToLower()).FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
                if (costForDelete == null) return null;
                context.Costs.Remove(costForDelete);
                costs.Add(costForDelete);
            }

            await context.SaveChangesAsync();

            return costs;
        }
    }
}