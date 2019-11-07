using System;

namespace CostIncomeCalculator.Dtos.LimitDtos
{
    /// <summary>
    /// Data transfer object of limit for return to user.
    /// Used for map original object to LimitReturnDto.
    /// Original object is <see cref="Models.Limit" />
    /// </summary>
    public class LimitReturnDto
    {
        /// <summary>
        /// User email in database.
        /// </summary>
        /// <value>string</value>
        public string Email { get; set; }

        /// <summary>
        /// Category of limit.
        /// </summary>
        /// <value>string</value>
        public string Category { get; set; }

        /// <summary>
        /// Value of limit.
        /// </summary>
        /// <value>decimal</value>
        public decimal Value { get; set; }

        /// <summary>
        /// Limit start date.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime From { get; set; }

        /// <summary>
        /// Limit end date.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime To { get; set; }
    }
}