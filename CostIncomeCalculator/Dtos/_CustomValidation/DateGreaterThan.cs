using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CostIncomeCalculator.Dtos._CustomValidation
{
    /// <summary>
    /// Date greater than checker class.
    /// </summary>
    public class DateGreaterThan : ValidationAttribute
    {
        private readonly string startDatePropertyName;

        /// <summary>
        /// Date greater than constructor.
        /// Examples of use:
        /// <see cref="CostIncomeCalculator.Dtos.LimitDtos.LimitForSetDto" />
        /// </summary>
        /// <param name="startDatePropertyName">Start date prop name for check.</param>
        public DateGreaterThan(string startDatePropertyName)
        {
            this.startDatePropertyName = startDatePropertyName;
        }

        /// <summary>
        /// IsValid method override.
        /// </summary>
        /// <param name="value">object</param>
        /// <param name="validationContext">Current validation context</param>
        /// <returns>If end date greater than start date return false, else true.</returns>
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