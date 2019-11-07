using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.LimitDtos
{
    /// <summary>
    /// Data transfer object for delete limit from database.
    /// </summary>
    public class LimitForDeleteDto
    {
        /// <summary>
        /// User email from database. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Identificators of rows for delete from database. Required.
        /// </summary>
        /// <value>int array</value>
        [Required]
        [MinLength(1)]
        public int[] Ids { get; set; }
    }
}