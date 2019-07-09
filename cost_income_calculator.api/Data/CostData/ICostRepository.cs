using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cost_income_calculator.api.Dtos.CostDtos;
using cost_income_calculator.api.Models;

namespace cost_income_calculator.api.Data.CostData
{
    public interface ICostRepository
    {
        Task<IEnumerable<CostReturnDto>> GetAllCosts(string username);
        Task<IEnumerable<CostReturnDto>> GetMonthlyCosts(string username, DateTime date);
        Task<Cost> SetCost(string username, string type, string description, double price, DateTime date);
        Task<Cost> EditCost(string username, int costId, string newType, string newDescription, double newPrice, DateTime newDate);
        Task<List<Cost>> DeleteCosts(string username, int[] costIds);
    }
}