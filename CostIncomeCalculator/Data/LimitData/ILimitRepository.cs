using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Dtos.LimitDtos;
using CostIncomeCalculator.Models;

namespace CostIncomeCalculator.Data.LimitData
{
    /// <summary>
    /// Limit repository interface
    /// </summary>
    public interface ILimitRepository
    {
        /// <summary>
        /// Get all limits from database.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Array of limits for concrete user from database.</returns>
        Task<IEnumerable<LimitReturnDto>> GetAllLimits(string username);

        /// <summary>
        /// Set limit to database.
        /// </summary>
        /// <param name="limitForSetDto"><see cref="LimitForSetDto"/></param>
        /// <returns>If success return created limit object, else throw exception.</returns>
        Task<Limit> SetLimit(LimitForSetDto limitForSetDto);

        /// <summary>
        /// Edit limit by id in database.
        /// </summary>
        /// <param name="limitId">Identifier of limit in database.</param>
        /// <param name="limitForEditDto"><see cref="LimitForEditDto"/></param>
        /// <returns>If success return updated limit object, else throw exception.</returns>
        Task<Limit> EditLimit(int limitId, LimitForEditDto limitForEditDto);
        
        /// <summary>
        /// Delete limits from database by ids.
        /// </summary>
        /// <param name="limitForDeleteDto"><see cref="LimitForDeleteDto" /></param>
        /// <returns>If success return deleted limits, else throw exception.</returns>
        Task<List<Limit>> DeleteLimits(LimitForDeleteDto limitForDeleteDto);
    }
}