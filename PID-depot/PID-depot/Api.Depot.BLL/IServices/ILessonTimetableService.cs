using Api.Depot.BLL.Dtos.LessonTimetableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.IServices
{
    public interface ILessonTimetableService
    {
        IEnumerable<LessonTimetableDto> GetTimetables();
        IEnumerable<LessonTimetableDto> GetLessonTimetables(int lessonId);
        LessonTimetableDto GetTimetable(int id);
        LessonTimetableDto GetLessonTimetable(int lessonId);
        bool DeleteLessonTimetable(int id);
        LessonTimetableDto CreateLessonTimetable(LessonTimetableCreationDto lessonTimetable);
        LessonTimetableDto UpdateLessonTimetable(LessonTimetableDto lessonTimetable);
    }
}
