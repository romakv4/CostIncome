using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.IncomeDtos
{
    /// <summary>
    /// Data transfer object for set income to database.
    /// <see cref="Data.IncomeData.IncomeRepository.SetIncome"/>
    /// </summary>
    public class IncomeForSetDto
    {
        /// <summary>
        /// Category of income to set. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Description of income to set.
        /// </summary>
        /// <value>string</value>
        public string Description { get; set; }

        /// <summary>
        /// Price of income. Required.
        /// </summary>
        /// <value>decimal</value>
        [Required]
        [Range(0.01, 999999999999)]
        public decimal Price { get; set; }

        /// <summary>
        /// Date of income. Required.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}