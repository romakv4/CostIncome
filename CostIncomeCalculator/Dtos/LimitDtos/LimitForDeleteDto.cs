using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.LimitDtos
{
    /// <summary>
    /// Data transfer object for delete limit from database.
    /// </summary>
    public class LimitForDeleteDto
    {
        /// <summary>
        /// Username from database. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Identificators of rows for delete from database. Required.
        /// </summary>
        /// <value>int array</value>
        [Required]
        public int[] Ids { get; set; }
    }
}