using System;

namespace Api.Depot.UIL.Models.Forms
{
    public class LessonDayForm
    {
        public int Day { get; set; }
        public string DayName { get; set; }
        public bool IsSelected { get; set; }
        public TimeSpan StartsAt { get; set; }
        public TimeSpan EndsAt { get; set; }
    }
}
