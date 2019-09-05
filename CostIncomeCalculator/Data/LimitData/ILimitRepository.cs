using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Dtos.LimitDtos;
using CostIncomeCalculator.Models;

namespace CostIncomeCalculator.Data.LimitData
{
    public interface ILimitRepository
    {
        Task<IEnumerable<LimitReturnDto>> GetAllLimits(string username);
        Task<Limit> SetLimit(LimitForSetDto limitForSetDto);
        Task<Limit> EditLimit(int limitId, LimitForEditDto limitForEditDto);
        Task<List<Limit>> DeleteLimits(LimitForDeleteDto limitForDeleteDto);
    }
}