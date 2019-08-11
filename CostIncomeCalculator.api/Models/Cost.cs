using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cost_income_calculator.api.Models
{
    /// <summary>
    /// Cost model.
    /// </summary>
    public class Cost
    {
        /// <summary>
        /// Unique cost identifier in database.
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
        /// Category of cost.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Description of cost. Maximum length 100 symbols.
        /// </summary>
        /// <value>string</value>
        [MaxLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Cost value.
        /// </summary>
        /// <value>decimal</value>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Date of cost.
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