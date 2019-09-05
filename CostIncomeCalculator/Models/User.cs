using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using cost_income_calculator.Models;

namespace cost_income_calculator
{
    /// <summary>
    /// User model
    /// </summary>
    public class User
    {   
        /// <summary>
        /// Unique user identifier in database.
        /// </summary>
        /// <value>integer</value>
        public int Id { get; set; }
        
        /// <summary>
        /// Required parameter username.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Username { get; set; }
        
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