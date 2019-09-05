using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cost_income_calculator.Models
{
    /// <summary>
    /// Limit model.
    /// </summary>
    public class Limit
    {   
        /// <summary>
        /// Unique identifier of limit in database.
        /// </summary>
        /// <value>integer</value>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to user.
        /// </summary>
        /// <value>integer</value>
        [ForeignKey("UserFK")]
        public int UserId { get; set; }

        /// <summary>
        /// Category of limit.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Value of limit.
        /// </summary>
        /// <value>decimal</value>
        [Required]
        public decimal Value { get; set; }

        /// <summary>
        /// Date of start limit.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        public DateTime From { get; set; }

        /// <summary>
        /// Date of end limit.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        public DateTime To { get; set; }

        /// <summary>
        /// Navigation field to user.
        /// </summary>
        public User user { get; set; }
    }
}