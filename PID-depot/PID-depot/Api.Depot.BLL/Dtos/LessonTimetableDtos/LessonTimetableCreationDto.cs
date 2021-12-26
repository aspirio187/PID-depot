using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.LessonTimetableDtos
{
    public class LessonTimetableCreationDto
    {
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public int LessonId { get; set; }

        public LessonTimetableEntity MapDAL()
        {
            return new LessonTimetableEntity()
            {
                StartsAt = StartsAt,
                EndsAt = EndsAt,
                LessonId = LessonId
            };
        }
    }
}
