using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Dtos.IncomeDtos;
using CostIncomeCalculator.Models;

namespace CostIncomeCalculator.Data.IncomeData
{
    /// <summary>
    /// Income repository interface.
    /// </summary>
    public interface IIncomeRepository
    {
        /// <summary>
        /// Get all user incomes. See implementation here <see cref="IncomeRepository.GetAllIncomes" />
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>Array of <see cref="IncomeReturnDto" /></returns>
        Task<IEnumerable<IncomeReturnDto>> GetAllIncomes(string username);

        /// <summary>
        /// Get concrete user income. See implementation here <see cref="IncomeRepository.GetConcreteIncome" />
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="id">Identificator of income in database.</param>
        /// <returns><see cref="IncomeReturnDto" /></returns>
        Task<IncomeReturnDto> GetConcreteIncome(string username, int id);

        /// <summary>
        /// Get weekly incomes. See implementation here <see cref="IncomeRepository.GetWeeklyIncomes" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="date">Date of the week</param>
        /// <param name="category">Category to get incomes. May be null.</param>
        /// <returns>Array of <see cref="IncomeReturnDto" /></returns>
        Task<IEnumerable<IncomeReturnDto>> GetWeeklyIncomes(string email, DateTime date, string category);

        /// <summary>
        /// Get monthly incomes. See implementation here <see cref="IncomeRepository.GetMonthlyIncomes" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="date">Date of the week</param>
        /// /// <param name="category">Category to get incomes. May be null.</param>
        /// <returns>Array of <see cref="IncomeReturnDto" /></returns>
        Task<IEnumerable<IncomeReturnDto>> GetMonthlyIncomes(string email, DateTime date, string category);

        /// <summary>
        /// Set income method. See implementation here <see cref="IncomeRepository.SetIncome" />.
        /// </summary>
        /// <param name="email">User email from JWT.</param>
        /// <param name="incomeForSetDto"><see cref="IncomeForSetDto" /></param>
        /// <returns><see cref="Income" /></returns>
        Task<Income> SetIncome(string email, IncomeForSetDto incomeForSetDto);

        /// <summary>
        /// Edit income method. See implementation here <see cref="IncomeRepository.EditIncome" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="incomeId">Identifier of income in database.</param>
        /// <param name="incomeForEditDto"><see cref="IncomeForEditDto" /></param>
        /// <returns>Edited <see cref="Income" /> object.</returns>
        Task<Income> EditIncome(string email, int incomeId, IncomeForEditDto incomeForEditDto);

        /// <summary>
        /// Delete income(s) method. See implementation here <see cref="IncomeRepository.DeleteIncomes" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="incomeForDeleteDto"><see cref="IncomeForDeleteDto" /></param>
        /// <returns>List of <see cref="Income" /></returns>
        Task<List<Income>> DeleteIncomes(string email, IncomeForDeleteDto incomeForDeleteDto);
    }
}