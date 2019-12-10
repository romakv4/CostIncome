using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CostIncomeCalculator.Dtos.IncomeDtos;
using CostIncomeCalculator.Helpers;
using CostIncomeCalculator.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CostIncomeCalculator.Data.IncomeData
{
    /// <summary>
    /// Income repository class.
    /// </summary>
    public class IncomeRepository : IIncomeRepository
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
        /// <param name="username">Username.</param>
        /// <returns>Array of <see cref="IncomeReturnDto" /></returns>
        public async Task<IEnumerable<IncomeReturnDto>> GetAllIncomes(string username)
        {
            try
            {
                List<Income> incomes = new List<Income>();

                incomes = await context.Incomes.ToListAsync();

                return mapper.Map<IEnumerable<IncomeReturnDto>>(incomes);
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
        /// <param name="username">Username.</param>
        /// <param name="id">Identificator of income in database.</param>
        /// <returns><see cref="IncomeReturnDto" /></returns>
        public async Task<IncomeReturnDto> GetConcreteIncome(string username, int id)
        {
            try
            {
                if (!await context.Incomes.AnyAsync(x => x.Id == id)) return null;

                var concreteIncome = await context.Incomes.Where(x => x.Id == id).SingleAsync();

                return mapper.Map<IncomeReturnDto>(concreteIncome);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get weekly incomes method.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="date">Date of the week</param>
        /// <returns>Array of <see cref="IncomeReturnDto" /></returns>
        public async Task<IEnumerable<IncomeReturnDto>> GetWeeklyIncomes(string email, DateTime date)
        {
            try
            {
                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(date);
                var weeklyIncomes = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

                return mapper.Map<IEnumerable<IncomeReturnDto>>(weeklyIncomes);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get weekly incomes by category method.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="date">Date of the week</param>
        /// <param name="category">Category to get incomes.</param>
        /// <returns>Array of <see cref="IncomeReturnDto" /></returns>
        public async Task<IEnumerable<IncomeReturnDto>> GetWeeklyIncomesByCategory(string email, DateTime date, string category)
        {
            try
            {
                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(date);
                var weeklyIncomesByCategory = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                    .Where(x => x.Category.ToLower() == category.ToLower()).ToListAsync();

                return mapper.Map<IEnumerable<IncomeReturnDto>>(weeklyIncomesByCategory);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get monthly incomes methos.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="date">Date of the week</param>
        /// <returns>Array of <see cref="IncomeReturnDto" /></returns>
        public async Task<IEnumerable<IncomeReturnDto>> GetMonthlyIncomes(string email, DateTime date)
        {
            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(date);
            var monthlyIncomes = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

            return mapper.Map<IEnumerable<IncomeReturnDto>>(monthlyIncomes);
        }

        /// <summary>
        /// Get monthly incomes by category method.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="date">Date of the week</param>
        /// <param name="category">Category to get incomes.</param>
        /// <returns>Array of <see cref="IncomeReturnDto" /></returns>
        public async Task<IEnumerable<IncomeReturnDto>> GetMonthlyIncomesByCategory(string email, DateTime date, string category)
        {
            try
            {
                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(date);
                var monthlyIncomesByCategory = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                    .Where(x => x.Category.ToLower() == category.ToLower()).ToListAsync();

                return mapper.Map<IEnumerable<IncomeReturnDto>>(monthlyIncomesByCategory);
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
        /// <param name="incomeForSetDto"><see cref="IncomeForSetDto" /></param>
        /// <returns><see cref="Income" /></returns>
        public async Task<Income> SetIncome(string email, IncomeForSetDto incomeForSetDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email.ToLower());

                var income = new Income
                {
                    UserId = user.Id,
                    Category = incomeForSetDto.Category,
                    Description = incomeForSetDto.Description,
                    Price = incomeForSetDto.Price,
                    Date = incomeForSetDto.Date
                };

                await context.AddAsync(income);
                await context.SaveChangesAsync();

                return income;
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
        /// <param name="incomeForEditDto"><see cref="IncomeForEditDto" /></param>
        /// <returns>Edited <see cref="Income" /> object.</returns>
        public async Task<Income> EditIncome(string email, int incomeId, IncomeForEditDto incomeForEditDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email.ToLower());

                if (!await context.Incomes.AnyAsync(x => x.Id == incomeId)) return null;

                var currentIncome = await context.Incomes.FirstOrDefaultAsync(x => x.Id == incomeId && x.UserId == user.Id);

                currentIncome.Category = incomeForEditDto.Category ?? currentIncome.Category;
                currentIncome.Description = incomeForEditDto.Description ?? currentIncome.Description;
                currentIncome.Price = incomeForEditDto.Price == 0 ? currentIncome.Price : incomeForEditDto.Price;
                currentIncome.Date = incomeForEditDto.Date == DateTime.MinValue ? currentIncome.Date : incomeForEditDto.Date;

                context.Incomes.Update(currentIncome);
                await context.SaveChangesAsync();

                return currentIncome;
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
        /// <param name="incomeForDeleteDto"><see cref="IncomeForDeleteDto" /></param>
        /// <returns>List of <see cref="Income" /></returns>
        public async Task<List<Income>> DeleteIncomes(string email, IncomeForDeleteDto incomeForDeleteDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email.ToLower());

                List<Income> incomes = new List<Income>();

                foreach (var incomeId in incomeForDeleteDto.Ids)
                {
                    var incomeForDelete = await context.Incomes.FirstOrDefaultAsync(x => x.Id == incomeId && x.UserId == user.Id);
                    if (incomeForDelete == null) return null;
                    context.Incomes.Remove(incomeForDelete);
                    incomes.Add(incomeForDelete);
                }

                await context.SaveChangesAsync();

                return incomes;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }
    }
}