using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Dtos;

namespace CostIncomeCalculator.Data.IncomeData
{
    public interface IIncomeRepository
    {
        Task<IEnumerable<AccountingItem>> GetAll(string email);
        Task<AccountingItem> GetConcrete(string email, int id);
        Task<AccountingItem> Set(string email, AccountingItemSetDto accountingItemSetDto);
        Task<AccountingItem> Edit(string email, int costId, AccountingItemEditDto accountingItemEditDto);
        Task<List<AccountingItem>> Delete(string email, AccountingItemDeleteDto accountingItemDeleteDto);
    }
}