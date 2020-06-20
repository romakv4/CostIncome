using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Dtos;

namespace CostIncomeCalculator.Data.CostData
{
    /// <summary>
    /// Cost repository interface.
    /// </summary>
    public interface ICostRepository
    {
        /// <summary>
        /// Get all user costs. See implementation here <see cref="CostRepository.GetAll" />
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Array of <see cref="AccountingItem" /></returns>
        Task<IEnumerable<AccountingItem>> GetAll(string email);

        /// <summary>
        /// Get concrete user cost. See implementation here <see cref="CostRepository.GetConcrete" />
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="id">Identificator of cost in database.</param>
        /// <returns><see cref="AccountingItem" /></returns>
        Task<AccountingItem> GetConcrete(string email, int id);

        /// <summary>
        /// Set cost method. See implementation here <see cref="CostRepository.Set" />.
        /// </summary>
        /// <param name="email">User email from JWT</param>
        /// <param name="accountingItemSetDto"><see cref="AccountingItem" /></param>
        /// <returns><see cref="AccountingItem" /></returns>
        Task<AccountingItem> Set(string email, AccountingItem accountingItemSetDto);

        /// <summary>
        /// Edit cost method. See implementation here <see cref="CostRepository.Edit" />.
        /// </summary>
        /// <param name="email">User email from JWT.</param>
        /// <param name="accountingItemEditDto"><see cref="AccountingItem" /></param>
        /// <returns>Edited <see cref="AccountingItem" /> object.</returns>
        Task<AccountingItem> Edit(string email, AccountingItem accountingItemEditDto);

        /// <summary>
        /// Delete cost(s) method. See implementation here <see cref="CostRepository.Delete" />.
        /// </summary>
        /// <param name="email">User email from JWT.</param>
        /// <param name="accountingItemDeleteDto"><see cref="AccountingItemDeleteDto" /></param>
        /// <returns>List of <see cref="AccountingItem" /></returns>
        Task<List<AccountingItem>> Delete(string email, AccountingItemDeleteDto accountingItemDeleteDto);
    }
}