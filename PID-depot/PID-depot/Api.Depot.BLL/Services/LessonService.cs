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
        private readonly IUserLessonRepository _userLessonRepository;
        private readonly IRoleRepository _roleRepository;

        public LessonService(ILessonRepository lessonRepository, IUserLessonRepository userLessonRepository, IRoleRepository roleRepository)
        {
            _lessonRepository = lessonRepository ??
                throw new ArgumentNullException(nameof(lessonRepository));
            _userLessonRepository = userLessonRepository ??
                throw new ArgumentNullException(nameof(userLessonRepository));
            _roleRepository = roleRepository ??
                throw new ArgumentNullException(nameof(roleRepository));
        }

        public bool AddLessonTeacher(int lessonId, Guid roleId, Guid userId)
        {
            if (lessonId <= 0) throw new ArgumentException(nameof(lessonId));
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));

            return _userLessonRepository.Create(new UserLessonEntity()
            {
                LessonId = lessonId,
                UserId = userId,
                RoleId = roleId
            }) > 0;
        }

        public LessonDto CreateLesson(LessonCreationDto lesson)
        {
            if (lesson is null) throw new ArgumentNullException(nameof(lesson));

            LessonEntity lessonToCreate = lesson.MapToDAL();
            int createdLessonId = _lessonRepository.Create(lessonToCreate);
            return _lessonRepository.GetById(createdLessonId).MapFromDAL();
        }

        public bool DeleteLesson(int id)
        {
            return _lessonRepository.Delete(id);
        }

        public LessonDto GetLesson(int id)
        {
            return _lessonRepository.GetById(id).MapFromDAL();
        }

        public IEnumerable<LessonDto> GetLessons()
        {
            return _lessonRepository.GetAll().Select(l => l.MapFromDAL());
        }

        public LessonDto UpdateLesson(LessonDto lesson)
        {
            if (lesson is null) throw new ArgumentNullException(nameof(lesson));

            LessonEntity lessonToUpdate = lesson.MapToDAL();
            return _lessonRepository.Update(lesson.Id, lessonToUpdate) ? lesson : null;
        }
    }
}
