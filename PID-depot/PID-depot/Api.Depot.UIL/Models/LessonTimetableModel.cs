using System;

namespace Api.Depot.UIL.Models
{
    public class LessonTimetableModel
    {
        public int Id { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public LessonModel Lesson { get; set; }
    }
}
