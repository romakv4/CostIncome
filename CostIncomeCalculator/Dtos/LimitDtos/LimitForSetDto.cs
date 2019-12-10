using System;
using System.ComponentModel.DataAnnotations;
using CostIncomeCalculator.Dtos._CustomValidation;

namespace CostIncomeCalculator.Dtos.LimitDtos
{
    /// <summary>
    /// Data transfer object for set limit to database.
    /// </summary>
    public class LimitForSetDto
    {
        /// <summary>
        /// Category of limit for set. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Value of current limit for set. Required.
        /// </summary>
        /// <value>decimal</value>
        [Required]
        public decimal Value { get; set; }

        /// <summary>
        /// Limit start date. Required.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        [Display(Name = "Limit start date")]
        public DateTime From { get; set; }

        /// <summary>
        /// Limit end date. Required.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        [DateGreaterThan("From")]
        [Display(Name = "Limit end date")]
        public DateTime To { get; set; }
    }
}