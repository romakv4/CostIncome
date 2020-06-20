using AutoMapper;
using CostIncomeCalculator.Dtos;
using CostIncomeCalculator.Dtos.LimitDtos;
using CostIncomeCalculator.Models;

namespace CostIncomeCalculator.Helpers
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
            CreateMap<Cost, AccountingItem>();
            CreateMap<Income, AccountingItem>();
            CreateMap<Limit, LimitReturnDto>();
        }
    }
}