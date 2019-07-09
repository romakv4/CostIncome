using AutoMapper;
using cost_income_calculator.api.Dtos.CostDtos;
using cost_income_calculator.api.Dtos.IncomeDtos;
using cost_income_calculator.api.Models;

namespace cost_income_calculator.api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cost, CostReturnDto>();
            CreateMap<Income, IncomeReturnDto>();
        }
    }
}