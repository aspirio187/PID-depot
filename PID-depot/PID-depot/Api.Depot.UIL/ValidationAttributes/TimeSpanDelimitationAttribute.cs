using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Api.Depot.UIL.ValidationAttributes
{
    public class TimeSpanDelimitationAttribute : ValidationAttribute
    {
        public int MinimumHours { get; set; }
        public int MinimumMinutes { get; set; }
        public int MaximumHours { get; set; }
        public int MaximumMinutes { get; set; }
        public string IsSelectedPropertyName { get; set; }

        public TimeSpanDelimitationAttribute(int minimumHours, int minimumMinutes, int maximumHours, int maximumMinutes,
            string isSelectedPropertyName = null)
        {
            if (minimumHours < 0 || minimumHours > 24)
                throw new ArgumentOutOfRangeException($"{nameof(minimumHours)} must be greater than or equals to 0 or smaller than 24");
            if (minimumMinutes < 0 || minimumMinutes > 60)
                throw new ArgumentOutOfRangeException($"{nameof(minimumMinutes)} must be greater than or equals to 0 or smaller than 60");
            if (maximumHours < 0 || maximumHours > 24)
                throw new ArgumentOutOfRangeException($"{nameof(maximumHours)} must be greater than or equals to 0 or smaller than 24");
            if (maximumMinutes < 0 || maximumMinutes > 60)
                throw new ArgumentOutOfRangeException($"{nameof(maximumMinutes)} must be greater than or equals to 0 or smaller than 60");

            if (isSelectedPropertyName is not null)
            {
                isSelectedPropertyName = isSelectedPropertyName.Trim();
                if (isSelectedPropertyName.Length == 0)
                    throw new ArgumentException($"{nameof(isSelectedPropertyName)} cannot be empty string!");
            }

            IsSelectedPropertyName = isSelectedPropertyName;
            MinimumHours = minimumHours;
            MinimumMinutes = minimumMinutes;
            MaximumHours = maximumHours;
            MaximumMinutes = maximumMinutes;
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

            TimeSpan actualTimeSpan = (TimeSpan)value;
            TimeSpan minimumTimeSpan = new TimeSpan(MinimumHours, MinimumMinutes, 0);
            TimeSpan maximumTimeSpan = new TimeSpan(MaximumHours, MaximumMinutes, 0);

            if (actualTimeSpan < minimumTimeSpan || actualTimeSpan > maximumTimeSpan)
                return new ValidationResult(base.ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
