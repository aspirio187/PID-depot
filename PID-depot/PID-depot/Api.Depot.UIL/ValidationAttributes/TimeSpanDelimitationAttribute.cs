using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Api.Depot.UIL.ValidationAttributes
{
    public class TimeSpanDelimitationAttribute : ValidationAttribute
    {
        public int MinimumHours { get; private set; }
        public int MinimumMinutes { get; private set; }
        public int MaximumHours { get; private set; }
        public int MaximumMinutes { get; private set; }
        public string IsSelectedPropertyName { get; private set; }

        /// <summary>
        /// Create a delimitation between two TimeSpan values defined by the minimum/maximum-hours and minutes values.
        /// </summary>
        /// <param name="minimumHours">Minimum TimeSpan hours</param>
        /// <param name="minimumMinutes">Minimum TimeSpan minutes</param>
        /// <param name="maximumHours">Maximum TimeSpan Hours</param>
        /// <param name="maximumMinutes">Maximum TimeSpan minutes</param>
        /// <param name="isSelectedPropertyName">Optional property to avoid the validation if this param is false</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if minimum/maximum-hours are smaller than 0 or greater than 24 and if minimum/maximum-hours are smaller
        /// than 0 or greater than 60
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="isSelectedPropertyName"/> is not null but is an empty string
        /// </exception>
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

        /// <summary>
        /// Verify if the minimum and maximum TimeSpan delimitation are correct. If <see cref="IsSelectedPropertyName"/> is not nulll it will retrieve
        /// its value and check : if IsSelected property is true the validation will go on, otherwise it will validate whatever the minimum/maximum are.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
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
