using System;

namespace CostIncomeCalculator.Dtos.CostDtos
{
    /// <summary>
    /// Data transfer object for return cost.
    /// <see cref="Data.CostData.CostRepository.GetAllCosts"/>
    /// <see cref="Data.CostData.CostRepository.GetConcreteCost"/>
    /// <see cref="Data.CostData.CostRepository.GetWeeklyCosts"/>
    /// <see cref="Data.CostData.CostRepository.GetMonthlyCosts"/>
    /// </summary>
    public class CostReturnDto
    {
        /// <summary>
        /// Unique identificator of cost in database.
        /// </summary>
        /// <value>int</value>
        public int Id { get; set; }
        
        /// <summary>
        /// Category of cost.
        /// </summary>
        /// <value>string</value>
        public string Category { get; set; }

        /// <summary>
        /// Description of cost.
        /// </summary>
        /// <value>string</value>
        public string Description { get; set; }

        /// <summary>
        /// Price of cost.
        /// </summary>
        /// <value>decimal</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Date of cost.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime Date { get; set; }
    }
}