using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Api.Depot.UIL.ValidationAttributes
{
    public class TimeSpanComparisonAttribute : ValidationAttribute
    {
        public string MinimumTimeSpanPropertyName { get; set; }

        public TimeSpanComparisonAttribute(string minimumTimeSpanPropertyName)
        {
            minimumTimeSpanPropertyName = minimumTimeSpanPropertyName.Trim();
            if (minimumTimeSpanPropertyName is null)
                throw new ArgumentNullException(nameof(minimumTimeSpanPropertyName));
            if (minimumTimeSpanPropertyName.Length == 0)
                throw new ArgumentException($"{nameof(minimumTimeSpanPropertyName)} cannot be empty string!");

            MinimumTimeSpanPropertyName = minimumTimeSpanPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo basePropertyInfo = validationContext.ObjectType.GetProperty(MinimumTimeSpanPropertyName);
            if (basePropertyInfo is null) throw new NullReferenceException($"{nameof(MinimumTimeSpanPropertyName)} doesn't exist!");

            TimeSpan minimumTimeSpan = (TimeSpan)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            TimeSpan actualTimeSpan = (TimeSpan)value;

            if (actualTimeSpan <= minimumTimeSpan) return new ValidationResult(base.ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
