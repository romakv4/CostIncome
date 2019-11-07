using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CostIncomeCalculator.Dtos.LimitDtos;
using CostIncomeCalculator.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CostIncomeCalculator.Data.LimitData
{
    /// <summary>
    /// Contains methods to work with limits in database.
    /// </summary>
    public class LimitRepository : ILimitRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        /// <summary>
        /// LimitRepository class constructor.
        /// </summary>
        /// <param name="context">Database context <see cref="DataContext"/></param>
        /// <param name="mapper"><see cref="AutoMapper"/></param>
        /// <param name="logger">Exceptions logger <see cref="Serilog"/></param>
        public LimitRepository(DataContext context, IMapper mapper, ILogger logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;

        }

        /// <summary>
        /// Get all limits from database.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Array of limits <see cref="LimitReturnDto" /> for concrete user from database.</returns>
        public async Task<IEnumerable<LimitReturnDto>> GetAllLimits(string username)
        {
            try
            {
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

        /// <summary>
        /// Set limit to database.
        /// </summary>
        /// <param name="limitForSetDto">Limit object for set <see cref="LimitForSetDto" /></param>
        /// <returns>If success return created limit object, else throw exception.</returns>
        public async Task<Limit> SetLimit(LimitForSetDto limitForSetDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == limitForSetDto.Email.ToLower());

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

        /// <summary>
        /// Edit limit by id in database.
        /// </summary>
        /// <param name="limitId">Identifier of limit in database.</param>
        /// <param name="limitForEditDto">Limit object for edit <see cref="LimitForEditDto" /></param>
        /// <returns>If success return updated limit object, else throw exception.</returns>
        public async Task<Limit> EditLimit(int limitId, LimitForEditDto limitForEditDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == limitForEditDto.Email.ToLower());

                if (!await context.Limits.AnyAsync(x => x.Id == limitId)) return null;

                var currentLimit = await context.Limits.FirstOrDefaultAsync(x => x.Id == limitId && x.UserId == user.Id);

                currentLimit.Category = limitForEditDto.Category ?? currentLimit.Category;
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

        /// <summary>
        /// Delete limits from database by ids.
        /// </summary>
        /// <param name="limitForDeleteDto">Limit object for delete <see cref="LimitForDeleteDto" /></param>
        /// <returns>If success return deleted limits, else throw exception.</returns>
        public async Task<List<Limit>> DeleteLimits(LimitForDeleteDto limitForDeleteDto)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == limitForDeleteDto.Email.ToLower());

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