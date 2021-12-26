using Api.Depot.BLL.Dtos.LessonDtos;
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
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository ??
                throw new ArgumentNullException(nameof(lessonRepository));
        }

        public LessonDto CreateLesson(LessonCreationDto lesson)
        {
            if (lesson is null) throw new ArgumentNullException(nameof(lesson));

            LessonEntity lessonToCreate = lesson.MapDAL();
            int createdLessonId = _lessonRepository.Create(lessonToCreate);
            return new LessonDto(_lessonRepository.GetById(createdLessonId));
        }

        public bool DeleteLesson(int id)
        {
            return _lessonRepository.Delete(id);
        }

        public LessonDto GetLesson(int id)
        {
            return new LessonDto(_lessonRepository.GetById(id));
        }

        public IEnumerable<LessonDto> GetLessons()
        {
            return _lessonRepository.GetAll().Select(l => new LessonDto(l));
        }

        public LessonDto UpdateLesson(LessonDto lesson)
        {
            if (lesson is null) throw new ArgumentNullException(nameof(lesson));

            LessonEntity lessonToUpdate = lesson.MapDAL();
            return _lessonRepository.Update(lesson.Id, lessonToUpdate) ? lesson : null;
        }
    }
}
