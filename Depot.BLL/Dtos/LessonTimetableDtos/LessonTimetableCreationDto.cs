using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.BLL.Dtos.LessonTimetableDtos
{
    public class LessonTimetableCreationDto
    {
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public int LessonId { get; set; }
    }
}
