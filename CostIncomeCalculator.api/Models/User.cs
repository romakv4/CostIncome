using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using cost_income_calculator.api.Models;

namespace cost_income_calculator.api
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }

        public List<Cost> Cost { get; set; }

        public List<Income> Income { get; set; }

        public List<Limit> Limit { get; set; }
    }
}