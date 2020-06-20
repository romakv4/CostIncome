using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos
{
    /// <summary>
    /// Data transfer object for delete accounting item from database.
    /// </summary>
    public class AccountingItemDeleteDto
    {
        /// <summary>
        /// Identificators of rows for delete from database. Required.
        /// </summary>
        /// <value></value>
        [Required]
        [MinLength(1)]
        public int[] Ids { get; set; }
    }
}