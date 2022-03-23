using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.ValidationAttributes
{
    public class TimeSpanDelimitationAttribute : ValidationAttribute
    {
        public int MinimumHours { get; set; }
        public int MinimumMinutes { get; set; }
        public int MaximumHours { get; set; }
        public int MaximumMinutes { get; set; }

        public TimeSpanDelimitationAttribute(int minimumHours, int minimumMinutes, int maximumHours, int maximumMinutes)
        {
            MinimumHours = minimumHours;
            MinimumMinutes = minimumMinutes;
            MaximumHours = maximumHours;
            MaximumMinutes = maximumMinutes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TimeSpan actualTimeSpan = (TimeSpan)value;
            TimeSpan minimumTimeSpan = new TimeSpan(MinimumHours, MinimumMinutes, 0);
            TimeSpan maximumTimeSpan = new TimeSpan(MaximumHours, MaximumMinutes, 0);

            if (actualTimeSpan < minimumTimeSpan || actualTimeSpan > maximumTimeSpan)
                return new ValidationResult(base.ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
