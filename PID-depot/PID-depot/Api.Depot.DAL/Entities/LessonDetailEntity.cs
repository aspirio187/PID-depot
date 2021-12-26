using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Entities
{
    public class LessonDetailEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int LessonTimetableId { get; set; }

        public LessonDetailEntity()
        {

        }
    }
}
