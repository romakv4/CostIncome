using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<LimitReturnDto>> GetAllLimits(string username)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

                List<Limit> limits = new List<Limit>();

                limits = await context.Limits.ToListAsync();

                return mapper.Map<IEnumerable<LimitReturnDto>>(limits);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
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

        public async Task<List<Limit>> DeleteLimits(LimitForDeleteDto limitForDeleteDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username == limitForDeleteDto.Username.ToLower());

                List<Limit> limits = new List<Limit>();

                foreach (var limitId in limitForDeleteDto.Ids)
                {
                    var limitForDelete = await context.Limits.FirstOrDefaultAsync(x => x.Id == limitId && x.UserId == user.Id);
                    if (limitForDelete == null) return null;
                    context.Limits.Remove(limitForDelete);
                    limits.Add(limitForDelete);
                }

                await context.SaveChangesAsync();

                return limits;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }
    }
}