using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos
{
    /// <summary>
    /// Accounting item model.
    /// </summary>
    public class AccountingItem
    {
        /// <summary>
        /// Unique accounting item identifier in database.
        /// </summary>
        /// <value>integer</value>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to user.
        /// </summary>
        /// <value>integer</value>
        public int UserId { get; set; }

        /// <summary>
        /// Category of accounting item.
        /// </summary>
        /// <value>string</value>
        [Required]
        [MaxLength(20)]
        public string Category { get; set; }

        /// <summary>
        /// Description of accounting item.
        /// </summary>
        /// <value>string</value>
        [MaxLength(20)]
        public string Description { get; set; }

        /// <summary>
        /// Accounting item price.
        /// </summary>
        /// <value>decimal</value>
        [Required]
        [Range(0.01, (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Date of accounting item.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        public DateTime Date { get; set; }
    }
}