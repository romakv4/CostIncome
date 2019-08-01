using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cost_income_calculator.api.Models
{
    public class Limit
    {
        public int Id { get; set; }

        [ForeignKey("UserFK")]
        public int UserId { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        public User user { get; set; }
    }
}