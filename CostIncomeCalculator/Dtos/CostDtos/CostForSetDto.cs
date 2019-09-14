using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.CostDtos
{
    /// <summary>
    /// Data transfer object for set cost to database.
    /// <see cref="Data.CostData.CostRepository.SetCost"/>
    /// </summary>
    public class CostForSetDto
    {
        /// <summary>
        /// Username from database. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Category of cost to set. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Description of cost to set. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Price of cost. Required.
        /// </summary>
        /// <value>decimal</value>
        [Required]
        [Range(0.01, 999999999999)]
        public decimal Price { get; set; }

        /// <summary>
        /// Date of cost. Required.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}