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

        public async Task<IEnumerable<CostReturnDto>> GetAllCosts(CostForGetDto costForGetDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == costForGetDto.Username.ToLower());

            List<Cost> costs = new List<Cost>();

            costs = await context.Costs.ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(costs);
        }

        public async Task<IEnumerable<CostReturnDto>> GetWeeklyCosts(PeriodicCostsDto periodicCostsDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetWeekDateRange(periodicCostsDto.Date);
            var weeklyCosts = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(weeklyCosts);
        }

        public async Task<IEnumerable<CostReturnDto>> GetWeeklyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetWeekDateRange(periodicCostsDto.Date);
            var weeklyCostsByCategory = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).Where(x => x.Type == category.ToLower()).ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(weeklyCostsByCategory);
        }

        public async Task<IEnumerable<CostReturnDto>> GetMonthlyCosts(PeriodicCostsDto periodicCostsDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicCostsDto.Date);
            var monthlyCosts = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(monthlyCosts);
        }

        public async Task<IEnumerable<CostReturnDto>> GetMonthlyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicCostsDto.Date);
            var monthlyCostsByCategory = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).Where(x => x.Type == category.ToLower()).ToListAsync();

            return mapper.Map<IEnumerable<CostReturnDto>>(monthlyCostsByCategory);
        }

        public async Task<MonthCostDto> GetMaxCostsCategoryInMonth(PeriodicCostsDto periodicCostsDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicCostsDto.Date);
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

        public async Task<Cost> SetCost(CostForSetDto costForSetDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == costForSetDto.Username.ToLower());

            var cost = new Cost
            {
                UserId = user.Id,
                Type = costForSetDto.Type.ToLower(),
                Description = costForSetDto.Description,
                Price = costForSetDto.Price,
                Date = costForSetDto.Date
            };

            await context.AddAsync(cost);
            await context.SaveChangesAsync();

            return cost;
        }

        public async Task<Cost> EditCost(int costId, CostForEditDto costForEditDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == costForEditDto.Username.ToLower());

            var currentCost = await context.Costs.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
            if (currentCost == null) return null;

            currentCost.Type = costForEditDto.Type.ToLower() ?? currentCost.Type;
            currentCost.Description = costForEditDto.Description ?? currentCost.Description;
            currentCost.Price = costForEditDto.Price == 0 ? currentCost.Price : costForEditDto.Price;

            context.Costs.Update(currentCost);
            await context.SaveChangesAsync();

            return currentCost;
        }

        public async Task<List<Cost>> DeleteCosts(CostForDeleteDto costForDeleteDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == costForDeleteDto.Username.ToLower());

            List<Cost> costs = new List<Cost>();

            foreach (var costId in costForDeleteDto.Ids)
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