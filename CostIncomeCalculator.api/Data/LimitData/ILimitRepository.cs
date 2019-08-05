using System.Collections.Generic;
using System.Threading.Tasks;
using cost_income_calculator.api.Dtos.LimitDtos;
using cost_income_calculator.api.Models;

namespace cost_income_calculator.api.Data.LimitData
{
    public interface ILimitRepository
    {
        Task<IEnumerable<LimitReturnDto>> GetAllLimits(string username);
        Task<Limit> SetLimit(LimitForSetDto limitForSetDto);
        Task<Limit> EditLimit(int limitId, LimitForEditDto limitForEditDto);
        Task<List<Limit>> DeleteLimits(LimitForDeleteDto limitForDeleteDto);
    }
}