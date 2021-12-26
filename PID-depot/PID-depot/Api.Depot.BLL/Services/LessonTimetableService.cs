using Api.Depot.BLL.Dtos.LessonTimetableDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.DAL.Entities;
using Api.Depot.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Services
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

            LessonTimetableEntity timetableToCreate = lessonTimetable.MapDAL();
            int timetableCreatedId = _lessonTimetableRepository.Create(timetableToCreate);
            return new LessonTimetableDto(_lessonTimetableRepository.GetById(timetableCreatedId));
        }

        public bool DeleteLessonTimetable(int id)
        {
            return _lessonTimetableRepository.Delete(id);
        }

        public LessonTimetableDto GetLessonTimetable(int lessonId, DateTime startsAt)
        {
            return new LessonTimetableDto(_lessonTimetableRepository.GetLessonTimetables(lessonId)
                .FirstOrDefault(lt => lt.StartsAt == startsAt));
        }

        public IEnumerable<LessonTimetableDto> GetLessonTimetables(int lessonId)
        {
            return _lessonTimetableRepository.GetLessonTimetables(lessonId).Select(lt => new LessonTimetableDto(lt));
        }

        public LessonTimetableDto GetTimetable(int id)
        {
            return new LessonTimetableDto(_lessonTimetableRepository.GetById(id));
        }

        public IEnumerable<LessonTimetableDto> GetTimetables()
        {
            return _lessonTimetableRepository.GetAll().Select(lt => new LessonTimetableDto(lt));
        }

        public LessonTimetableDto UpdateLessonTimetable(LessonTimetableDto lessonTimetable)
        {
            if (lessonTimetable is null) throw new ArgumentNullException(nameof(lessonTimetable));

            LessonTimetableEntity timetableToUpdate = lessonTimetable.MapDAL();
            return _lessonTimetableRepository.Update(lessonTimetable.Id, timetableToUpdate) ? lessonTimetable : null;
        }
    }
}
