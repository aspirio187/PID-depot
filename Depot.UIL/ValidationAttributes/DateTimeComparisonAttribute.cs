using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Depot.UIL.ValidationAttributes
{
    public class DateTimeComparisonAttribute : ValidationAttribute
    {
        public string MinimumDatePropertyName { get; set; }

        public DateTimeComparisonAttribute(string minimumDatePropertyName)
        {
            MinimumDatePropertyName = minimumDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo basePropertyInfo = validationContext.ObjectType.GetProperty(MinimumDatePropertyName);
            DateTime minimumDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            DateTime comparisonDate = (DateTime)value;

            if (minimumDate >= comparisonDate) return new ValidationResult(base.ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
