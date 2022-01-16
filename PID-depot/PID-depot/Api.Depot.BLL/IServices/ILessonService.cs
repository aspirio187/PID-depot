using Api.Depot.BLL.Dtos.LessonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.IServices
{
    public interface ILessonService
    {
        IEnumerable<LessonDto> GetLessons();
        LessonDto GetLesson(int id);
        bool DeleteLesson(int id);
        LessonDto CreateLesson(LessonCreationDto lesson);
        LessonDto UpdateLesson(LessonDto lesson);
        bool AddLessonTeacher(int lessonId, Guid userId);
    }
}
