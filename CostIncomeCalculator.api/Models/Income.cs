using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cost_income_calculator.api.Models
{
    public class Income
    {
        public int Id { get; set; }

        [ForeignKey("UserFK")]
        public int UserId { get; set; }

        [Required]
        public string Category { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public User user { get; set; }
    }
}