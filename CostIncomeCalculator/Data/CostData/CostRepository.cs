using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CostIncomeCalculator.Dtos.CostDtos;
using CostIncomeCalculator.Helpers;
using CostIncomeCalculator.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CostIncomeCalculator.Data.CostData
{
    /// <summary>
    /// Cost repository class.
    /// </summary>
    public class CostRepository : ICostRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IDatesHelper datesHelper;
        private readonly ILogger logger;

        /// <summary>
        /// Cost repository constructor.
        /// </summary>
        /// <param name="context"><see cref="DataContext" /></param>
        /// <param name="mapper"><see cref="AutoMapperProfiles" /></param>
        /// <param name="datesHelper"><see cref="DatesHelper" /></param>
        /// <param name="logger">Serilog logger.</param>
        public CostRepository(DataContext context, IMapper mapper, IDatesHelper datesHelper, ILogger logger)
        {
            this.logger = logger;
            this.datesHelper = datesHelper;
            this.mapper = mapper;
            this.context = context;
        }
        
        /// <summary>
        /// Get all user costs method.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>Array of <see cref="CostReturnDto" /></returns>
        public async Task<IEnumerable<CostReturnDto>> GetAllCosts(string username)
        {
            try
            {
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

        /// <summary>
        /// Get concrete user cost method.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="id">Identificator of cost in database.</param>
        /// <returns><see cref="CostReturnDto" /></returns>
        public async Task<CostReturnDto> GetConcreteCost(string username, int id)
        {
            try
            {
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

        /// <summary>
        /// Get weekly costs method.
        /// </summary>
        /// <param name="periodicCostsDto"><see cref="PeriodicCostsDto" /></param>
        /// <returns>Array of <see cref="CostReturnDto" /></returns>
        public async Task<IEnumerable<CostReturnDto>> GetWeeklyCosts(PeriodicCostsDto periodicCostsDto)
        {
            try
            {
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

        /// <summary>
        /// Get weekly costs by category method.
        /// </summary>
        /// <param name="periodicCostsDto"><see cref="PeriodicCostsDto" /></param>
        /// <param name="category">Category to get costs.</param>
        /// <returns>Array of <see cref="CostReturnDto" /></returns>
        public async Task<IEnumerable<CostReturnDto>> GetWeeklyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category)
        {
            try
            {
                (DateTime, DateTime) dates = datesHelper.GetWeekDateRange(periodicCostsDto.Date);
                var weeklyCostsByCategory = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                .Where(x => x.Category.ToLower() == category.ToLower()).ToListAsync();

                return mapper.Map<IEnumerable<CostReturnDto>>(weeklyCostsByCategory);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get monthly costs method.
        /// </summary>
        /// <param name="periodicCostsDto"><see cref="PeriodicCostsDto" /></param>
        /// <returns>Array of <see cref="CostReturnDto" /></returns>
        public async Task<IEnumerable<CostReturnDto>> GetMonthlyCosts(PeriodicCostsDto periodicCostsDto)
        {
            try
            {
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

        /// <summary>
        /// Get monthly costs by category method.
        /// </summary>
        /// <param name="periodicCostsDto"><see cref="PeriodicCostsDto" /></param>
        /// <param name="category">Category to get costs.</param>
        /// <returns>Array of <see cref="CostReturnDto" /></returns>
        public async Task<IEnumerable<CostReturnDto>> GetMonthlyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category)
        {
            try
            {
                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicCostsDto.Date);
                var monthlyCostsByCategory = await context.Costs.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                .Where(x => x.Category.ToLower() == category.ToLower()).ToListAsync();

                return mapper.Map<IEnumerable<CostReturnDto>>(monthlyCostsByCategory);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Set cost method.
        /// </summary>
        /// <param name="costForSetDto"><see cref="CostForSetDto" /></param>
        /// <returns><see cref="Cost" /></returns>
        public async Task<Cost> SetCost(CostForSetDto costForSetDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == costForSetDto.Email.ToLower());

                var cost = new Cost
                {
                    UserId = user.Id,
                    Category = costForSetDto.Category,
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

        /// <summary>
        /// Edit cost method.
        /// </summary>
        /// <param name="costId">Identifier of cost in database.</param>
        /// <param name="costForEditDto"><see cref="CostForEditDto" /></param>
        /// <returns>Edited <see cref="Cost" /> object.</returns>
        public async Task<Cost> EditCost(int costId, CostForEditDto costForEditDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == costForEditDto.Email.ToLower());

                if (!await context.Costs.AnyAsync(x => x.Id == costId)) return null;

                var currentCost = await context.Costs.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);

                currentCost.Category = costForEditDto.Category ?? currentCost.Category;
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

        /// <summary>
        /// Delete cost(s) method.
        /// </summary>
        /// <param name="costForDeleteDto"><see cref="CostForDeleteDto" /></param>
        /// <returns>List of <see cref="Cost" /></returns>
        public async Task<List<Cost>> DeleteCosts(CostForDeleteDto costForDeleteDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == costForDeleteDto.Email.ToLower());

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
