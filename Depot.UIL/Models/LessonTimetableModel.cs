using System;

namespace Depot.UIL.Models
{
    public class LessonTimetableModel
    {
        public int Id { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public int LessonId { get; set; }
    }
}
