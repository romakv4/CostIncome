using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cost_income_calculator.api.Dtos.IncomeDtos;
using cost_income_calculator.api.Helpers;
using cost_income_calculator.api.Models;
using Microsoft.EntityFrameworkCore;

namespace cost_income_calculator.api.Data.IncomeData
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IDatesHelper datesHelper;

        public IncomeRepository(DataContext context, IMapper mapper, IDatesHelper datesHelper)
        {
            this.datesHelper = datesHelper;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<IncomeReturnDto>> GetAllIncomes(IncomeForGetDto incomeForGetDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == incomeForGetDto.Username.ToLower());

            List<Income> incomes = new List<Income>();

            incomes = await context.Incomes.ToListAsync();

            return mapper.Map<IEnumerable<IncomeReturnDto>>(incomes);
        }

        public async Task<IEnumerable<IncomeReturnDto>> GetWeeklyIncomes(PeriodicIncomesDto periodicIncomesDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicIncomesDto.Username.ToLower());

            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicIncomesDto.Date);
            var weeklyIncomes = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();

            return mapper.Map<IEnumerable<IncomeReturnDto>>(weeklyIncomes);
        }

        public async Task<IEnumerable<IncomeReturnDto>> GetWeeklyIncomesByCategory(PeriodicIncomesDto periodicIncomesDto, string category)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicIncomesDto.Username.ToLower());

            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicIncomesDto.Date);
            var weeklyIncomesByCategory = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                .Where(x => x.Type == category.ToLower()).ToListAsync();

            return mapper.Map<IEnumerable<IncomeReturnDto>>(weeklyIncomesByCategory);
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
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicIncomesDto.Username.ToLower());

            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicIncomesDto.Date);
            var monthlyIncomesByCategory = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date)
                                                                 .Where(x => x.Type == category.ToLower()).ToListAsync();

            return mapper.Map<IEnumerable<IncomeReturnDto>>(monthlyIncomesByCategory);
        }

        public async Task<MonthIncomeDto> GetMaxIncomesCategoryInMonth(PeriodicIncomesDto periodicIncomesDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == periodicIncomesDto.Username.ToLower());
            
            (DateTime, DateTime) dates = datesHelper.GetMonthDateRange(periodicIncomesDto.Date);
            var monthlyIncomes = await context.Incomes.Where(x => x.Date >= dates.Item1.Date && x.Date <= dates.Item2.Date).ToListAsync();
            var categories = monthlyIncomes.Select(x => x.Type).Distinct();
            
            List<MonthIncomeDto> costs = new List<MonthIncomeDto>();
            foreach (var category in categories)
            {
                costs.Add(new MonthIncomeDto { 
                    Type = category.ToLower(), 
                    IncomeSum = monthlyIncomes.Where(x => x.Type == category.ToLower()).Select(x => x.Price).Sum() 
                    });
            }

            return costs.FirstOrDefault(x => x.IncomeSum == costs.Max(z => z.IncomeSum));
        }

        public async Task<Income> SetIncome(IncomeForSetDto incomeForSetDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == incomeForSetDto.Username.ToLower());

            var income = new Income
            {
                UserId = user.Id,
                Type = incomeForSetDto.Type.ToLower(),
                Description = incomeForSetDto.Description,
                Price = incomeForSetDto.Price,
                Date = incomeForSetDto.Date
            };

            await context.AddAsync(income);
            await context.SaveChangesAsync();

            return income;
        }

        public async Task<Income> EditIncome(int costId, IncomeForEditDto incomeForEditDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == incomeForEditDto.Username.ToLower());

            var currentIncome = await context.Incomes.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
            if (currentIncome == null) return null;

            currentIncome.Type = incomeForEditDto.Type.ToLower() ?? currentIncome.Type;
            currentIncome.Description = incomeForEditDto.Description ?? currentIncome.Description;
            currentIncome.Price = incomeForEditDto.Price == 0 ? currentIncome.Price : incomeForEditDto.Price;
            currentIncome.Date = incomeForEditDto.Date == DateTime.MinValue ? currentIncome.Date: incomeForEditDto.Date;

            context.Incomes.Update(currentIncome);
            await context.SaveChangesAsync();

            return currentIncome;
        }

        public async Task<List<Income>> DeleteIncomes(IncomeForDeleteDto incomeForDeleteDto)
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
    }
}