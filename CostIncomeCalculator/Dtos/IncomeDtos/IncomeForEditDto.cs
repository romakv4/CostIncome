using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.IncomeDtos
{
    /// <summary>
    /// Data transfer object for edit income in database.
    /// <see cref="Data.IncomeData.IncomeRepository.EditIncome"/>
    /// </summary>
    public class IncomeForEditDto
    {
        /// <summary>
        /// Category of income for edit.
        /// </summary>
        /// <value>string</value>
        public string Category { get; set; }

        /// <summary>
        /// Description of income for edit.
        /// </summary>
        /// <value>string</value>
        public string Description { get; set; }

        /// <summary>
        /// Value to change income price.
        /// </summary>
        /// <value>decimal</value>
        public decimal Price { get; set; }

        /// <summary>
        /// New income date.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime Date { get; set; }
    }
}