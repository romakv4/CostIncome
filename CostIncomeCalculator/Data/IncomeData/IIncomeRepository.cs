using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Dtos;

namespace CostIncomeCalculator.Data.IncomeData
{
    /// <summary>
    /// Income repository interface.
    /// </summary>
    public interface IIncomeRepository
    {
        /// <summary>
        /// Get all user incomes. See implementation here <see cref="IncomeRepository.GetAll" />
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Array of <see cref="AccountingItem" /></returns>
        Task<IEnumerable<AccountingItem>> GetAll(string email);

        /// <summary>
        /// Get concrete user income. See implementation here <see cref="IncomeRepository.GetConcrete" />
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="id">Identificator of income in database.</param>
        /// <returns><see cref="AccountingItem" /></returns>
        Task<AccountingItem> GetConcrete(string email, int id);

        /// <summary>
        /// Set income method. See implementation here <see cref="IncomeRepository.Set" />.
        /// </summary>
        /// <param name="email">User email from JWT.</param>
        /// <param name="accountingItemSetDto"><see cref="AccountingItemSetDto" /></param>
        /// <returns><see cref="AccountingItem" /></returns>
        Task<AccountingItem> Set(string email, AccountingItemSetDto accountingItemSetDto);

        /// <summary>
        /// Edit income method. See implementation here <see cref="IncomeRepository.Edit" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="incomeId">Identifier of income in database.</param>
        /// <param name="accountingItemEditDto"><see cref="AccountingItemEditDto" /></param>
        /// <returns>Edited <see cref="AccountingItem" /> object.</returns>
        Task<AccountingItem> Edit(string email, int incomeId, AccountingItemEditDto accountingItemEditDto);

        /// <summary>
        /// Delete income(s) method. See implementation here <see cref="IncomeRepository.Delete" />.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="accountingItemDeleteDto"><see cref="AccountingItemDeleteDto" /></param>
        /// <returns>List of <see cref="AccountingItem" /></returns>
        Task<List<AccountingItem>> Delete(string email, AccountingItemDeleteDto accountingItemDeleteDto);
    }
}