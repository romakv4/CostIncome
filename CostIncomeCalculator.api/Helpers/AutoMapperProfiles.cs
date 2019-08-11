using AutoMapper;
using cost_income_calculator.api.Dtos.CostDtos;
using cost_income_calculator.api.Dtos.IncomeDtos;
using cost_income_calculator.api.Dtos.LimitDtos;
using cost_income_calculator.api.Models;

namespace cost_income_calculator.api.Helpers
{  
    /// <summary>
    /// <see cref="AutoMapper"/> profiles for mapping models on dto.
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        /// <summary>
        /// AutoMapper profiles set.
        /// </summary>
        public AutoMapperProfiles()
        {
            CreateMap<Cost, CostReturnDto>();
            CreateMap<Income, IncomeReturnDto>();
            CreateMap<Limit, LimitReturnDto>();
        }
    }
}