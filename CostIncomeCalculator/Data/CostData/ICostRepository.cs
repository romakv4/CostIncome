using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Dtos.CostDtos;
using CostIncomeCalculator.Models;

namespace CostIncomeCalculator.Data.CostData
{
    /// <summary>
    /// Cost repository interface.
    /// </summary>
    public interface ICostRepository
    {
        /// <summary>
        /// Get all user costs. See implementation here <see cref="CostRepository.GetAllCosts" />
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>Array of <see cref="CostReturnDto" /></returns>
        Task<IEnumerable<CostReturnDto>> GetAllCosts(string username);

        /// <summary>
        /// Get concrete user cost. See implementation here <see cref="CostRepository.GetConcreteCost" />
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="id">Identificator of cost in database.</param>
        /// <returns><see cref="CostReturnDto" /></returns>
        Task<CostReturnDto> GetConcreteCost(string username, int id);

        /// <summary>
        /// Get weekly costs. See implementation here <see cref="CostRepository.GetWeeklyCosts" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="date">Date of the week</param>
        /// <param name="category">Category to get costs. May be null.</param>
        /// <returns>Array of <see cref="CostReturnDto" /></returns>
        Task<IEnumerable<CostReturnDto>> GetWeeklyCosts(string email, DateTime date, string category);

        /// <summary>
        /// Get monthly costs. See implementation here <see cref="CostRepository.GetMonthlyCosts" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="date">Date of the month</param>
        /// <param name="category">Category to get costs. May be null.</param>
        /// <returns>Array of <see cref="CostReturnDto" /></returns>
        Task<IEnumerable<CostReturnDto>> GetMonthlyCosts(string email, DateTime date, string category);
        
        /// <summary>
        /// Set cost method. See implementation here <see cref="CostRepository.SetCost" />.
        /// </summary>
        /// <param name="email">User email from JWT</param>
        /// <param name="costForSetDto"><see cref="CostForSetDto" /></param>
        /// <returns><see cref="Cost" /></returns>
        Task<Cost> SetCost(string email, CostForSetDto costForSetDto);
        
        /// <summary>
        /// Edit cost method. See implementation here <see cref="CostRepository.EditCost" />.
        /// </summary>
        /// <param name="email">User email from JWT.</param>
        /// <param name="costId">Identifier of cost in database.</param>
        /// <param name="costForEditDto"><see cref="CostForEditDto" /></param>
        /// <returns>Edited <see cref="Cost" /> object.</returns>
        Task<Cost> EditCost(string email, int costId, CostForEditDto costForEditDto);
        
        /// <summary>
        /// Delete cost(s) method. See implementation here <see cref="CostRepository.DeleteCosts" />.
        /// </summary>
        /// <param name="email">User email from JWT.</param>
        /// <param name="costForDeleteDto"><see cref="CostForDeleteDto" /></param>
        /// <returns>List of <see cref="Cost" /></returns>
        Task<List<Cost>> DeleteCosts(string email, CostForDeleteDto costForDeleteDto);
    }
}