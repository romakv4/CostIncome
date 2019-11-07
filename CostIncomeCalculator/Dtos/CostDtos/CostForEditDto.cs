using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.CostDtos
{
    /// <summary>
    /// Data transfer object for edit cost in database.
    /// <see cref="Data.CostData.CostRepository.EditCost"/>
    /// </summary>
    public class CostForEditDto
    {
        /// <summary>
        /// User email from database. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Category of cost for edit.
        /// </summary>
        /// <value>string</value>
        public string Category { get; set; }

        /// <summary>
        /// Description of cost for edit.
        /// </summary>
        /// <value>string</value>
        public string Description { get; set; }

        /// <summary>
        /// Value to change cost price.
        /// </summary>
        /// <value>decimal</value>
        public decimal Price { get; set; }

        /// <summary>
        /// New cost date.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime Date { get; set; }
    }
}