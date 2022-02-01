using Api.Depot.BLL.Dtos.LessonDetailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.IServices
{
    public interface ILessonDetailService
    {
        IEnumerable<LessonDetailDto> GetLessonDetails(int lessonTimetableId);
        IEnumerable<LessonDetailDto> GetDetails();
        LessonDetailDto GetDetail(int id);
        LessonDetailDto GetLessonDetail(int lessonTimetableId);
        bool DeleteLessonDetail(int id);
        LessonDetailDto CreateLessonDetail(LessonDetailCreationDto lessonDetail);
        LessonDetailDto UpdateLessonDetail(LessonDetailDto lessonDetail);
    }
}
