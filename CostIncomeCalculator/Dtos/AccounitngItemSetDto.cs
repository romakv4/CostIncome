using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos
{
    /// <summary>
    /// Data transfer object for set accounting item to database.
    /// </summary>
    public class AccountingItemSetDto
    {

        /// <summary>
        /// Category of accounting item to set. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Description of accounting item to set.
        /// </summary>
        /// <value>string</value>
        public string Description { get; set; }

        /// <summary>
        /// Price of accounting item. Required.
        /// </summary>
        /// <value>decimal</value>
        [Required]
        [Range(0.01, 999999999999)]
        public decimal Price { get; set; }

        /// <summary>
        /// Date of accounting item. Required.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}