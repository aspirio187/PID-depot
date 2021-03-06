using Depot.BLL.Dtos.LessonTimetableDtos;
using Depot.BLL.IServices;
using Depot.DAL.Entities;
using Depot.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.BLL.Services
{
    public class LessonTimetableService : ILessonTimetableService
    {
        private readonly ILessonTimetableRepository _lessonTimetableRepository;

        public LessonTimetableService(ILessonTimetableRepository lessonTimetableRepository)
        {
            _lessonTimetableRepository = lessonTimetableRepository ??
                throw new ArgumentNullException(nameof(lessonTimetableRepository));
        }

        public LessonTimetableDto CreateLessonTimetable(LessonTimetableCreationDto lessonTimetable)
        {
            if (lessonTimetable is null) throw new ArgumentNullException(nameof(lessonTimetable));

            LessonTimetableEntity timetableToCreate = lessonTimetable.MapToDAL();
            int timetableCreatedId = _lessonTimetableRepository.Create(timetableToCreate);
            return _lessonTimetableRepository.GetById(timetableCreatedId).MapFromDAL();
        }

        public bool DeleteLessonTimetable(int id)
        {
            return _lessonTimetableRepository.Delete(id);
        }

        public LessonTimetableDto GetLessonTimetable(int lessonId, DateTime startsAt)
        {
            return _lessonTimetableRepository.GetLessonTimetables(lessonId).FirstOrDefault(lt => lt.StartsAt == startsAt).MapFromDAL();
        }

        public IEnumerable<LessonTimetableDto> GetLessonTimetables(int lessonId)
        {
            return _lessonTimetableRepository.GetLessonTimetables(lessonId).Select(lt => lt.MapFromDAL());
        }

        public LessonTimetableDto GetTimetable(int id)
        {
            return _lessonTimetableRepository.GetById(id).MapFromDAL();
        }

        public IEnumerable<LessonTimetableDto> GetTimetables()
        {
            return _lessonTimetableRepository.GetAll().Select(lt => lt.MapFromDAL());
        }

        public LessonTimetableDto UpdateLessonTimetable(LessonTimetableDto lessonTimetable)
        {
            if (lessonTimetable is null) throw new ArgumentNullException(nameof(lessonTimetable));

            LessonTimetableEntity timetableToUpdate = lessonTimetable.MapToDAL();
            return _lessonTimetableRepository.Update(lessonTimetable.Id, timetableToUpdate) ? lessonTimetable : null;
        }
    }
}
