using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Api.Depot.UIL.ValidationAttributes
{
    public class TimeSpanComparisonAttribute : ValidationAttribute
    {
        public string MinimumTimeSpanPropertyName { get; set; }
        public string IsSelectedPropertyName { get; set; }

        public TimeSpanComparisonAttribute(string minimumTimeSpanPropertyName, string isSelectedPropertyName = null)
        {
            minimumTimeSpanPropertyName = minimumTimeSpanPropertyName.Trim();

            if (minimumTimeSpanPropertyName is null)
                throw new ArgumentNullException(nameof(minimumTimeSpanPropertyName));
            if (minimumTimeSpanPropertyName.Length == 0)
                throw new ArgumentException($"{nameof(minimumTimeSpanPropertyName)} cannot be empty string!");

            if (isSelectedPropertyName is not null)
            {
                isSelectedPropertyName = isSelectedPropertyName.Trim();
                if (isSelectedPropertyName.Length == 0)
                    throw new ArgumentException($"{nameof(isSelectedPropertyName)} cannot be empty string!");
            }

            IsSelectedPropertyName = isSelectedPropertyName;
            MinimumTimeSpanPropertyName = minimumTimeSpanPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsSelectedPropertyName is not null)
            {
                PropertyInfo isSelectedPropertyInfo = validationContext.ObjectType.GetProperty(IsSelectedPropertyName);
                if (isSelectedPropertyInfo is null) throw new NullReferenceException($"{nameof(IsSelectedPropertyName)} doesn't exist!");

                bool isSelected = (bool)isSelectedPropertyInfo.GetValue(value, null);

                if (isSelected == false) return ValidationResult.Success;
            }

            PropertyInfo basePropertyInfo = validationContext.ObjectType.GetProperty(MinimumTimeSpanPropertyName);
            if (basePropertyInfo is null) throw new NullReferenceException($"{nameof(MinimumTimeSpanPropertyName)} doesn't exist!");

            TimeSpan minimumTimeSpan = (TimeSpan)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            TimeSpan actualTimeSpan = (TimeSpan)value;

            if (actualTimeSpan <= minimumTimeSpan) return new ValidationResult(base.ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
