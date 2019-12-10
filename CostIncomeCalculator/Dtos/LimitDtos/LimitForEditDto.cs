using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.LimitDtos
{
    /// <summary>
    /// Data transfer object for edit limit in database.
    /// </summary>
    public class LimitForEditDto
    {
        /// <summary>
        /// Category of limit for edit.
        /// </summary>
        /// <value>string</value>
        public string Category { get; set; }

        /// <summary>
        /// Value to change limit.
        /// </summary>
        /// <value>decimal</value>
        public decimal Value { get; set; }

        /// <summary>
        ///  New limit start date.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime From { get; set; }

        /// <summary>
        /// New limit end date.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime To { get; set; }
    }
}