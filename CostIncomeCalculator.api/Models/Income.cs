using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cost_income_calculator.api.Models
{   
    /// <summary>
    /// Income model.
    /// </summary>
    public class Income
    {
        /// <summary>
        /// Unique identifier of income in database.
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
        /// Category of income.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Description of income. Maximum length 100 symbols.
        /// </summary>
        /// <value>string</value>
        [MaxLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Value of income.
        /// </summary>
        /// <value>decimal</value>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Date of income.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Navigation field to user.
        /// </summary>
        public User user { get; set; }
    }
}