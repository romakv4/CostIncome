using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cost_income_calculator.Dtos.IncomeDtos;
using cost_income_calculator.Helpers;
using cost_income_calculator.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace cost_income_calculator.Data.IncomeData
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IDatesHelper datesHelper;
        private readonly ILogger logger;

        public IncomeRepository(DataContext context, IMapper mapper, IDatesHelper datesHelper, ILogger logger)
        {
            this.logger = logger;
            this.datesHelper = datesHelper;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<IncomeReturnDto>> GetAllIncomes(string username)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

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

        public async Task<IncomeReturnDto> GetConcreteIncome(string username, int id)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

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

        public async Task<IEnumerable<IncomeReturnDto>> GetWeeklyIncomes(PeriodicIncomesDto periodicIncomesDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicIncomesDto.Username.ToLower());

                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicIncomesDto.Date);
                var weeklyIncomes = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

                return mapper.Map<IEnumerable<IncomeReturnDto>>(weeklyIncomes);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<IncomeReturnDto>> GetWeeklyIncomesByCategory(PeriodicIncomesDto periodicIncomesDto, string category)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicIncomesDto.Username.ToLower());

                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicIncomesDto.Date);
                var weeklyIncomesByCategory = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                    .Where(x => x.Category == category.ToLower()).ToListAsync();

                return mapper.Map<IEnumerable<IncomeReturnDto>>(weeklyIncomesByCategory);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<IncomeReturnDto>> GetMonthlyIncomes(PeriodicIncomesDto periodicIncomesDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicIncomesDto.Username.ToLower());

            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicIncomesDto.Date);
            var monthlyIncomes = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

            return mapper.Map<IEnumerable<IncomeReturnDto>>(monthlyIncomes);
        }

        public async Task<IEnumerable<IncomeReturnDto>> GetMonthlyIncomesByCategory(PeriodicIncomesDto periodicIncomesDto, string category)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicIncomesDto.Username.ToLower());

                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicIncomesDto.Date);
                var monthlyIncomesByCategory = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                    .Where(x => x.Category == category.ToLower()).ToListAsync();

                return mapper.Map<IEnumerable<IncomeReturnDto>>(monthlyIncomesByCategory);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<MonthIncomeDto> GetMaxIncomesCategoryInMonth(PeriodicIncomesDto periodicIncomesDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicIncomesDto.Username.ToLower());

                (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicIncomesDto.Date);
                var monthlyIncomes = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();
                var categories = monthlyIncomes.Select(x => x.Category).Distinct();

                List<MonthIncomeDto> costs = new List<MonthIncomeDto>();
                foreach (var category in categories)
                {
                    costs.Add(new MonthIncomeDto
                    {
                        Category = category.ToLower(),
                        IncomeSum = monthlyIncomes.Where(x => x.Category == category.ToLower()).Select(x => x.Price).Sum()
                    });
                }

                return costs.FirstOrDefault(x => x.IncomeSum == costs.Max(z => z.IncomeSum));
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }

        public async Task<Income> SetIncome(IncomeForSetDto incomeForSetDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == incomeForSetDto.Username.ToLower());

                var income = new Income
                {
                    UserId = user.Id,
                    Category = incomeForSetDto.Category.ToLower(),
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

        public async Task<Income> EditIncome(int incomeId, IncomeForEditDto incomeForEditDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == incomeForEditDto.Username.ToLower());

                if (!await context.Incomes.AnyAsync(x => x.Id == incomeId)) return null;

                var currentIncome = await context.Incomes.FirstOrDefaultAsync(x => x.Id == incomeId && x.UserId == user.Id);

                currentIncome.Category = incomeForEditDto.Category.ToLower() ?? currentIncome.Category;
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

        public async Task<List<Income>> DeleteIncomes(IncomeForDeleteDto incomeForDeleteDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == incomeForDeleteDto.Username.ToLower());

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