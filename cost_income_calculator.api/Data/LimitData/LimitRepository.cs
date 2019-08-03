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
                    Category = limitForSetDto.Category,
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

        public async Task<Limit> EditLimit(int limitId, LimitForEditDto limitForEditDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == limitForEditDto.Username.ToLower());

                if (!await context.Limits.AnyAsync(x => x.Id == limitId)) return null;

                var currentLimit = await context.Limits.FirstOrDefaultAsync(x => x.Id == limitId && x.UserId == user.Id);

                currentLimit.Category = currentLimit.Category.ToLower() ?? currentLimit.Category;
                currentLimit.Value = limitForEditDto.Value == 0 ? currentLimit.Value : limitForEditDto.Value;
                currentLimit.From = limitForEditDto.From == DateTime.MinValue ? currentLimit.From : limitForEditDto.From;
                currentLimit.To = limitForEditDto.To == DateTime.MinValue ? currentLimit.To : limitForEditDto.To;

                context.Limits.Update(currentLimit);
                await context.SaveChangesAsync();

                return currentLimit;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }
    }
}