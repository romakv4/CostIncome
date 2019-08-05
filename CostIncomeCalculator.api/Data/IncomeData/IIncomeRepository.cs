using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cost_income_calculator.api.Dtos.IncomeDtos;
using cost_income_calculator.api.Models;

namespace cost_income_calculator.api.Data.IncomeData
{
    public interface IIncomeRepository
    {
        Task<IEnumerable<IncomeReturnDto>> GetAllIncomes(string username);
        Task<IncomeReturnDto> GetConcreteIncome(string username, int id);
        Task<IEnumerable<IncomeReturnDto>> GetWeeklyIncomes(PeriodicIncomesDto periodicIncomesDto);
        Task<IEnumerable<IncomeReturnDto>> GetWeeklyIncomesByCategory(PeriodicIncomesDto periodicIncomesDto, string category);
        Task<IEnumerable<IncomeReturnDto>> GetMonthlyIncomes(PeriodicIncomesDto periodicIncomesDto);
        Task<IEnumerable<IncomeReturnDto>> GetMonthlyIncomesByCategory(PeriodicIncomesDto periodicIncomesDto, string category);
        Task<MonthIncomeDto> GetMaxIncomesCategoryInMonth(PeriodicIncomesDto periodicIncomesDto);
        Task<Income> SetIncome(IncomeForSetDto incomeForSetDto);
        Task<Income> EditIncome(int costId, IncomeForEditDto incomeForEditDto);
        Task<List<Income>> DeleteIncomes(IncomeForDeleteDto incomeForDeleteDto);
    }
}