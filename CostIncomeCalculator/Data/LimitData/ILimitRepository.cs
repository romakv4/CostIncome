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
        /// Get all limits from database. See implementation here <see cref="LimitRepository.GetAllLimits" />.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Array of limits <see cref="LimitReturnDto" /> for concrete user from database.</returns>
        Task<IEnumerable<LimitReturnDto>> GetAllLimits(string username);

        /// <summary>
        /// Set limit to database. See implementation here <see cref="LimitRepository.SetLimit" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="limitForSetDto">Limit object for set <see cref="LimitForSetDto" /></param>
        /// <returns>If success return created limit object, else throw exception.</returns>
        Task<Limit> SetLimit(string email, LimitForSetDto limitForSetDto);

        /// <summary>
        /// Edit limit by id in database. See implementation here <see cref="LimitRepository.EditLimit" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="limitId">Identifier of limit in database.</param>
        /// <param name="limitForEditDto">Limit object for edit <see cref="LimitForEditDto" /></param>
        /// <returns>If success return updated limit object, else throw exception.</returns>
        Task<Limit> EditLimit(string email, int limitId, LimitForEditDto limitForEditDto);
        
        /// <summary>
        /// Delete limits from database by ids. See implementation here <see cref="LimitRepository.DeleteLimits" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="limitForDeleteDto">Limit object for delete <see cref="LimitForDeleteDto" /></param>
        /// <returns>If success return deleted limits, else throw exception.</returns>
        Task<List<Limit>> DeleteLimits(string email, LimitForDeleteDto limitForDeleteDto);
    }
}