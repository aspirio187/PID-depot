using Api.Depot.BLL.Dtos.LessonDtos;
using Api.Depot.BLL.Dtos.UserDtos;
using Api.Depot.BLL.Dtos.UserLessonDtos;
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
        UserDto GetLessonTeacher(int lessonId);
        bool DeleteLesson(int id);
        LessonDto CreateLesson(LessonCreationDto lesson);
        LessonDto UpdateLesson(LessonDto lesson);
        bool AddLessonTeacher(UserLessonCreationDto userLesson);
    }
}
