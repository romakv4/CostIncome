using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.CostDtos
{
    /// <summary>
    /// Data transfer object for delete cost from database.
    /// <see cref="Data.CostData.CostRepository.DeleteCosts" />
    /// </summary>
    public class CostForDeleteDto
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