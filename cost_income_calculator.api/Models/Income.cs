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
        public string Type { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public User user { get; set; }
    }
}