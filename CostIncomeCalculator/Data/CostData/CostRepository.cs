using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cost_income_calculator.Dtos.CostDtos;
using cost_income_calculator.Helpers;
using cost_income_calculator.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace cost_income_calculator.Data.CostData
{
    public class CostRepository : ICostRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IDatesHelper datesHelper;
        private readonly ILogger logger;
        public CostRepository(DataContext context, IMapper mapper, IDatesHelper datesHelper, ILogger logger)
        {
            this.logger = logger;
            this.datesHelper = datesHelper;
            this.mapper = mapper;
            this.context = context;
        }
        
        public async Task<IEnumerable<CostReturnDto>> GetAllCosts(string username)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

                List<Cost> costs = new List<Cost>();

                costs = await context.Costs.ToListAsync();

                return mapper.Map<IEnumerable<CostReturnDto>>(costs);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<CostReturnDto> GetConcreteCost(string username, int id)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

                if (!await context.Costs.AnyAsync(x => x.Id == id)) return null;

                var concreteCost = await context.Costs.Where(x => x.Id == id).SingleAsync();

                return mapper.Map<CostReturnDto>(concreteCost);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<CostReturnDto>> GetWeeklyCosts(PeriodicCostsDto periodicCostsDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());

                (DateTime, DateTime) dates = datesHelper.GetWeekDateRange(periodicCostsDto.Date);
                var weeklyCosts = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

                return mapper.Map<IEnumerable<CostReturnDto>>(weeklyCosts);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<CostReturnDto>> GetWeeklyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());

                (DateTime, DateTime) dates = datesHelper.GetWeekDateRange(periodicCostsDto.Date);
                var weeklyCostsByCategory = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                .Where(x => x.Category == category.ToLower()).ToListAsync();

                return mapper.Map<IEnumerable<CostReturnDto>>(weeklyCostsByCategory);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<CostReturnDto>> GetMonthlyCosts(PeriodicCostsDto periodicCostsDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());

                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicCostsDto.Date);
                var monthlyCosts = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

                return mapper.Map<IEnumerable<CostReturnDto>>(monthlyCosts);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<CostReturnDto>> GetMonthlyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());

                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicCostsDto.Date);
                var monthlyCostsByCategory = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                .Where(x => x.Category == category.ToLower()).ToListAsync();

                return mapper.Map<IEnumerable<CostReturnDto>>(monthlyCostsByCategory);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<MonthCostDto> GetMaxCostsCategoryInMonth(PeriodicCostsDto periodicCostsDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicCostsDto.Username.ToLower());

                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicCostsDto.Date);
                var monthlyCosts = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();
                var categories = monthlyCosts.Select(x => x.Category).Distinct();

                List<MonthCostDto> costs = new List<MonthCostDto>();
                foreach (var category in categories)
                {
                    costs.Add(new MonthCostDto
                    {
                        Category = category.ToLower(),
                        CostSum = monthlyCosts.Where(x => x.Category == category.ToLower()).Select(x => x.Price).Sum()
                    });
                }

                return costs.FirstOrDefault(x => x.CostSum == costs.Max(z => z.CostSum));
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<Cost> SetCost(CostForSetDto costForSetDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == costForSetDto.Username.ToLower());

                var cost = new Cost
                {
                    UserId = user.Id,
                    Category = costForSetDto.Category.ToLower(),
                    Description = costForSetDto.Description,
                    Price = costForSetDto.Price,
                    Date = costForSetDto.Date
                };

                await context.AddAsync(cost);
                await context.SaveChangesAsync();

                return cost;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<Cost> EditCost(int costId, CostForEditDto costForEditDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == costForEditDto.Username.ToLower());

                if (!await context.Costs.AnyAsync(x => x.Id == costId)) return null;

                var currentCost = await context.Costs.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);

                currentCost.Category = costForEditDto.Category.ToLower() ?? currentCost.Category;
                currentCost.Description = costForEditDto.Description ?? currentCost.Description;
                currentCost.Price = costForEditDto.Price == 0 ? currentCost.Price : costForEditDto.Price;
                currentCost.Date = costForEditDto.Date == DateTime.MinValue ? currentCost.Date : costForEditDto.Date;

                context.Costs.Update(currentCost);
                await context.SaveChangesAsync();

                return currentCost;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<List<Cost>> DeleteCosts(CostForDeleteDto costForDeleteDto)
        {
            try
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
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }
    }
}