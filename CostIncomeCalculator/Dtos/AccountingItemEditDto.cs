using System;

namespace CostIncomeCalculator.Dtos
{
    /// <summary>
    /// Data transfer object for edit accounting item in database.
    /// </summary>
    public class AccountingItemEditDto
    {
        /// <summary>
        /// Category of accounting item for edit.
        /// </summary>
        /// <value>string</value>
        public string Category { get; set; }

        /// <summary>
        /// Description of accounting item for edit.
        /// </summary>
        /// <value>string</value>
        public string Description { get; set; }

        /// <summary>
        /// Value to change accounting item price.
        /// </summary>
        /// <value>decimal</value>
        public decimal Price { get; set; }

        /// <summary>
        /// New accounting item date.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime Date { get; set; }
    }
}