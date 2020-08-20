using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CostIncomeCalculator.Dtos;
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
        /// <param name="email">User email.</param>
        /// <returns>Array of <see cref="AccountingItem" /></returns>
        public async Task<IEnumerable<AccountingItem>> GetAll(string email)
        {
            try
            {
                List<Cost> costs = new List<Cost>();

                costs = await context.Costs.Where(x => x.user.Email == email).OrderByDescending(x => x.Id).ToListAsync();

                return mapper.Map<IEnumerable<AccountingItem>>(costs);
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
        /// <param name="email">User email.</param>
        /// <param name="id">Identificator of cost in database.</param>
        /// <returns><see cref="Cost" /></returns>
        public async Task<AccountingItem> GetConcrete(string email, int id)
        {
            try
            {
                if (!await context.Costs.AnyAsync(x => x.Id == id)) return null;

                var concreteCost = await context.Costs
                                        .Where(x => 
                                                x.user.Email == email &&
                                                x.Id == id
                                        ).SingleAsync();

                return mapper.Map<AccountingItem>(concreteCost);
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
        /// <param name="email">User email</param>
        /// <param name="costForSetDto"><see cref="AccountingItem" /></param>
        /// <returns><see cref="Cost" /></returns>
        public async Task<AccountingItem> Set(string email, AccountingItem costForSetDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);

                var cost = new Cost
                {
                    UserId = user.Id,
                    Category = costForSetDto.Category,
                    Description = costForSetDto.Description,
                    Price = costForSetDto.Price,
                    Date = costForSetDto.Date
                };

                await context.Costs.AddAsync(cost);
                await context.SaveChangesAsync();

                return mapper.Map<AccountingItem>(cost);
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
        /// <param name="email">User email</param>
        /// <param name="costForEditDto"><see cref="AccountingItem" /></param>
        /// <returns>Edited <see cref="Cost" /> object.</returns>
        public async Task<AccountingItem> Edit(string email, AccountingItem costForEditDto)
        {
            try
            {
                if (!await context.Costs.AnyAsync(x => x.Id == costForEditDto.Id)) return null;

                var currentCost = await context.Costs.FirstOrDefaultAsync(x => x.Id == costForEditDto.Id && x.user.Email == email);

                currentCost.Category = costForEditDto.Category ?? currentCost.Category;
                currentCost.Description = costForEditDto.Description ?? currentCost.Description;
                currentCost.Price = costForEditDto.Price == 0 ? currentCost.Price : costForEditDto.Price;
                currentCost.Date = costForEditDto.Date == DateTime.MinValue ? currentCost.Date : costForEditDto.Date;

                context.Costs.Update(currentCost);
                await context.SaveChangesAsync();

                return mapper.Map<AccountingItem>(currentCost);
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
        /// <param name="email">User email</param>
        /// <param name="costForDeleteDto"><see cref="AccountingItemDeleteDto" /></param>
        /// <returns>List of <see cref="Cost" /></returns>
        public async Task<List<AccountingItem>> Delete(string email, AccountingItemDeleteDto costForDeleteDto)
        {
            try
            {
                List<Cost> costs = new List<Cost>();

                foreach (var costId in costForDeleteDto.Ids)
                {
                    var costForDelete = await context.Costs.FirstOrDefaultAsync(x => x.Id == costId && x.user.Email == email);
                    if (costForDelete == null) return null;
                    context.Costs.Remove(costForDelete);
                    costs.Add(costForDelete);
                }

                await context.SaveChangesAsync();

                return mapper.Map<List<AccountingItem>>(costs);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }
    }
}
