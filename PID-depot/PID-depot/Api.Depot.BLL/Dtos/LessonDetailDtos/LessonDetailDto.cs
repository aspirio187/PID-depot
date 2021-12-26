using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.LessonDetailDtos
{
    public class LessonDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int LessonTimetableId { get; set; }

        public LessonDetailDto()
        {

        }
    }
}
