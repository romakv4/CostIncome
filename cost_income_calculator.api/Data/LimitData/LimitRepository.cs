using System;
using System.Threading.Tasks;
using AutoMapper;
using cost_income_calculator.api.Dtos.LimitDtos;
using cost_income_calculator.api.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace cost_income_calculator.api.Data.LimitData
{
    public class LimitRepository : ILimitRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        public LimitRepository(DataContext context, IMapper mapper, ILogger logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;

        }
        public async Task<Limit> SetLimit(LimitForSetDto limitForSetDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == limitForSetDto.Username.ToLower());

                var limit = new Limit
                {
                    UserId = user.Id,
                    Value = limitForSetDto.Value,
                    From = limitForSetDto.From,
                    To = limitForSetDto.To
                };

                await context.AddAsync(limit);
                await context.SaveChangesAsync();

                return limit;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }
    }
}