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

namespace CostIncomeCalculator.Data.IncomeData
{
    /// <summary>
    /// Income repository class.
    /// </summary>
    public class IncomeRepository : IAccountingItemRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IDatesHelper datesHelper;
        private readonly ILogger logger;

        /// <summary>
        /// Income repository constructor.
        /// </summary>
        /// <param name="context"><see cref="DataContext" /></param>
        /// <param name="mapper"><see cref="AutoMapperProfiles" /></param>
        /// <param name="datesHelper"><see cref="DatesHelper" /></param>
        /// <param name="logger">Serilog logger.</param>
        public IncomeRepository(DataContext context, IMapper mapper, IDatesHelper datesHelper, ILogger logger)
        {
            this.logger = logger;
            this.datesHelper = datesHelper;
            this.mapper = mapper;
            this.context = context;
        }

        /// <summary>
        /// Get all user incomes method.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Array of <see cref="AccountingItem" /></returns>
        public async Task<IEnumerable<AccountingItem>> GetAll(string email)
        {
            try
            {
                List<Income> incomes = new List<Income>();

                incomes = await context.Incomes.Where(x => x.user.Email == email).OrderByDescending(x => x.Id).ToListAsync();

                return mapper.Map<List<AccountingItem>>(incomes);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get concrete user income method.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="id">Identificator of income in database.</param>
        /// <returns><see cref="AccountingItem" /></returns>
        public async Task<AccountingItem> GetConcrete(string email, int id)
        {
            try
            {
                if (!await context.Incomes.AnyAsync(x => x.Id == id)) return null;

                var concreteIncome = await context.Incomes
                                        .Where(x =>
                                                x.user.Email == email &&
                                                x.Id == id
                                        ).SingleAsync();

                return mapper.Map<AccountingItem>(concreteIncome);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Set income method.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="incomeForSetDto"><see cref="AccountingItemSetDto" /></param>
        /// <returns><see cref="Income" /></returns>
        public async Task<AccountingItem> Set(string email, AccountingItemSetDto incomeForSetDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);

                var income = new Cost
                {
                    UserId = user.Id,
                    Category = incomeForSetDto.Category,
                    Description = incomeForSetDto.Description,
                    Price = incomeForSetDto.Price,
                    Date = incomeForSetDto.Date
                };

                await context.AddAsync(income);
                await context.SaveChangesAsync();

                return mapper.Map<AccountingItem>(income);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Edit income method.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="incomeId">Identifier of income in database.</param>
        /// <param name="incomeForEditDto"><see cref="AccountingItemEditDto" /></param>
        /// <returns>Edited <see cref="Income" /> object.</returns>
        public async Task<AccountingItem> Edit(string email, int incomeId, AccountingItemEditDto incomeForEditDto)
        {
            try
            {
                if (!await context.Incomes.AnyAsync(x => x.Id == incomeId)) return null;

                var currentIncome = await context.Incomes.FirstOrDefaultAsync(x => x.Id == incomeId && x.user.Email == email);

                currentIncome.Category = incomeForEditDto.Category ?? currentIncome.Category;
                currentIncome.Description = incomeForEditDto.Description ?? currentIncome.Description;
                currentIncome.Price = incomeForEditDto.Price == 0 ? currentIncome.Price : incomeForEditDto.Price;
                currentIncome.Date = incomeForEditDto.Date == DateTime.MinValue ? currentIncome.Date : incomeForEditDto.Date;

                context.Incomes.Update(currentIncome);
                await context.SaveChangesAsync();

                return mapper.Map<AccountingItem>(currentIncome);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Delete income(s) method.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="incomeForDeleteDto"><see cref="AccountingItemDeleteDto" /></param>
        /// <returns>List of <see cref="Income" /></returns>
        public async Task<List<AccountingItem>> Delete(string email, AccountingItemDeleteDto incomeForDeleteDto)
        {
            try
            {
                List<Income> incomes = new List<Income>();

                foreach (var incomeId in incomeForDeleteDto.Ids)
                {
                    var incomeForDelete = await context.Incomes.FirstOrDefaultAsync(x => x.Id == incomeId && x.user.Email == email);
                    if (incomeForDelete == null) return null;
                    context.Incomes.Remove(incomeForDelete);
                    incomes.Add(incomeForDelete);
                }

                await context.SaveChangesAsync();

                return mapper.Map<List<AccountingItem>>(incomes);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }
    }
}