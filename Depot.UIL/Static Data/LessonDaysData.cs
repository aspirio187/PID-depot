using Depot.UIL.Helpers;
using Depot.UIL.Models.Forms;
using System;
using System.Collections.Generic;

namespace Depot.UIL.Static_Data
{
    public static class LessonDaysData
    {
        public static IEnumerable<LessonDayForm> LoadLessonDays()
        {
            List<LessonDayForm> lessonDays = new List<LessonDayForm>();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                if (day != DayOfWeek.Sunday)
                {
                    lessonDays.Add(new LessonDayForm()
                    {
                        Day = (int)day,
                        DayName = DateTimeHelper.DayOfWeekToFrench(day),
                    });
                }
            }

            return lessonDays;
        }
    }
}
