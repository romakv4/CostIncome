using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CostIncomeCalculator.Dtos._CustomValidation
{
    public class DateGreaterThan : ValidationAttribute
    {
        private readonly string startDatePropertyName;
        public DateGreaterThan(string startDatePropertyName)
        {
            this.startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(this.startDatePropertyName);

            if (propertyInfo == null) 
                return new ValidationResult($"Unknown property {this.startDatePropertyName}");
            
            var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            if ((DateTime)value > (DateTime)propertyValue) 
            {
                return ValidationResult.Success;
            }
            else
            {
                var startDateDisplayName = propertyInfo
                    .GetCustomAttributes(typeof(DisplayAttribute), true)
                    .Cast<DisplayAttribute>()
                    .Single()
                    .Name;
                
                return new ValidationResult($"{validationContext.DisplayName} must be later than {startDateDisplayName}.");
            }
        }
    }
}