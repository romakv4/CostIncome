using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Models
{
    /// <summary>
    /// User model.
    /// </summary>
    public class User
    {   
        /// <summary>
        /// Unique user identifier in database.
        /// </summary>
        /// <value>integer</value>
        public int Id { get; set; }
        
        /// <summary>
        /// Required parameter email.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Email { get; set; }
        
        /// <summary>
        /// Auto generated parameter password hash. Required.
        /// Generation occurs during the registration process.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// One to many field. Relationship between user and cost table.
        /// </summary>
        public List<Cost> Cost { get; set; }

        /// <summary>
        /// One to many field. Relationship between user and income table.
        /// </summary>
        public List<Income> Income { get; set; }

        /// <summary>
        /// One to many field. Relationship between user and limit table.
        /// </summary>
        public List<Limit> Limit { get; set; }
    }
}