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

        public LessonDetailDto(LessonDetailEntity lessonDetail)
        {
            Id = lessonDetail.Id;
            Title = lessonDetail.Title;
            Details = lessonDetail.Details;
            LessonTimetableId = lessonDetail.LessonTimetableId;
        }

        public LessonDetailEntity MapDAL()
        {
            return new LessonDetailEntity()
            {
                Id = Id,
                Title = Title,
                Details = Details,
                LessonTimetableId = LessonTimetableId
            };
        }

        public static implicit operator LessonDetailDto(LessonDetailEntity lessonDetail)
        {
            return new LessonDetailDto(lessonDetail);
        }

        public static implicit operator LessonDetailEntity(LessonDetailDto lessonDetail)
        {
            return new LessonDetailEntity()
            {
                Id = lessonDetail.Id,
                Title = lessonDetail.Title,
                Details = lessonDetail.Details,
                LessonTimetableId = lessonDetail.LessonTimetableId
            };
        }
    }
}
