using System;

namespace Api.Depot.UIL.Helpers
{
    public static class DateTimeHelper
    {
        public static string DayOfWeekToFrench(DayOfWeek dayOfWeek) => dayOfWeek switch
        {
            DayOfWeek.Sunday => "Dimanche",
            DayOfWeek.Monday => "Lundi",
            DayOfWeek.Tuesday => "Mardi",
            DayOfWeek.Wednesday => "Mercredi",
            DayOfWeek.Thursday => "Jeudi",
            DayOfWeek.Friday => "Vendredi",
            DayOfWeek.Saturday => "Samedi",
            _ => string.Empty,
        };
    }
}
