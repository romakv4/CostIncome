using AutoMapper;
using cost_income_calculator.Dtos.CostDtos;
using cost_income_calculator.Dtos.IncomeDtos;
using cost_income_calculator.Dtos.LimitDtos;
using cost_income_calculator.Models;

namespace cost_income_calculator.Helpers
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