using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.IncomeDtos
{
    /// <summary>
    /// Data transfer object for delete income from database.
    /// <see cref="Data.IncomeData.IncomeRepository.DeleteIncomes" />
    /// </summary>
    public class IncomeForDeleteDto
    {
        /// <summary>
        /// Username from database. Required.
        /// </summary>
        /// <value>int</value>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Identificators of rows for delete from database. Required.
        /// </summary>
        /// <value></value>
        [Required]
        [MinLength(1)]
        public int[] Ids { get; set; }
    }
}