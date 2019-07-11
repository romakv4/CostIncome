using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cost_income_calculator.api.Dtos.CostDtos;
using cost_income_calculator.api.Models;

namespace cost_income_calculator.api.Data.CostData
{
    public interface ICostRepository
    {
        Task<IEnumerable<CostReturnDto>> GetAllCosts(CostForGetDto costForGetDto);
        Task<IEnumerable<CostReturnDto>> GetWeeklyCosts(PeriodicCostsDto periodicCostsDto);
        Task<IEnumerable<CostReturnDto>> GetWeeklyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category);
        Task<IEnumerable<CostReturnDto>> GetMonthlyCosts(PeriodicCostsDto periodicCostsDto);
        Task<IEnumerable<CostReturnDto>> GetMonthlyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category);
        Task<MonthCostDto> GetMaxCostsCategoryInMonth(PeriodicCostsDto periodicCostsDto);
        Task<Cost> SetCost(CostForSetDto costForSetDto);
        Task<Cost> EditCost(int costId, CostForEditDto costForEditDto);
        Task<List<Cost>> DeleteCosts(CostForDeleteDto costForDeleteDto);
    }
}