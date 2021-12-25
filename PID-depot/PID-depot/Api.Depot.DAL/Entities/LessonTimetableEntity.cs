using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Entities
{
    public class LessonTimetableEntity
    {
        public int Id { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public int LessonId { get; set; }

        public LessonTimetableEntity()
        {

        }

        public LessonTimetableEntity(IDataRecord data)
        {
            Id = (int)data["id"];
            StartsAt = (DateTime)data["starts_at"];
            EndsAt = (DateTime)data["ends_at"];
            LessonId = (int)data["lesson_id"];
        }
    }
}
