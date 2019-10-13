using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.CostDtos
{
    /// <summary>
    /// Data transfer object for get periodic costs.
    /// <see cref="Data.CostData.CostRepository.GetWeeklyCosts" />
    /// <see cref="Data.CostData.CostRepository.GetWeeklyCostsByCategory" />
    /// <see cref="Data.CostData.CostRepository.GetMonthlyCosts" />
    /// <see cref="Data.CostData.CostRepository.GetMonthlyCostsByCategory" />
    /// </summary>
    public class PeriodicCostsDto
    {
        /// <summary>
        /// Username from database. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Username { get;set; }
        
        /// <summary>
        /// Date for periodic functions work.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        public DateTime Date { get; set; }
    }
}