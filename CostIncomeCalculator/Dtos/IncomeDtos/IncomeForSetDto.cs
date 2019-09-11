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
        /// Username from database. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Category of income to set. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Description of income to set. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Price of income. Required.
        /// </summary>
        /// <value>decimal</value>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Date of income. Required.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        public DateTime Date { get; set; }
    }
}