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
        Task<Income> SetIncome(string username, string type, string description, double price, DateTime date);
        Task<Income> EditIncome(string username, int costId, string newType, string newDescription, double newPrice, DateTime newDate);
        Task<Income> DeleteIncome(string username, int costId);
    }
}