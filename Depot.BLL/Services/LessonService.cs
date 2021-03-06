using Depot.BLL.Dtos.LessonDtos;
using Depot.BLL.Dtos.UserDtos;
using Depot.BLL.Dtos.UserLessonDtos;
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
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public LessonService(ILessonRepository lessonRepository, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _lessonRepository = lessonRepository ??
                throw new ArgumentNullException(nameof(lessonRepository));
            _roleRepository = roleRepository ??
                throw new ArgumentNullException(nameof(roleRepository));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        public bool AddLessonUser(int lessonId, Guid userId)
        {
            if (lessonId <= 0) throw new ArgumentOutOfRangeException(nameof(lessonId));
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));

            return _lessonRepository.AddLessonUser(lessonId, userId);
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

        public bool DeleteLessonUser(int lessonId, Guid userId)
        {
            return _lessonRepository.DeleteLessonUser(lessonId, userId);
        }

        public LessonDto GetLesson(int id)
        {
            return _lessonRepository.GetById(id).MapFromDAL();
        }

        public IEnumerable<LessonDto> GetLessons()
        {
            return _lessonRepository.GetAll().Select(l => l.MapFromDAL());
        }

        public IEnumerable<LessonDto> GetUserLessons(Guid userId)
        {
            return _lessonRepository.GetUserLessons(userId).Select(l => l.MapFromDAL());
        }

        public LessonDto UpdateLesson(LessonDto lesson)
        {
            if (lesson is null) throw new ArgumentNullException(nameof(lesson));

            LessonEntity lessonToUpdate = lesson.MapToDAL();
            return _lessonRepository.Update(lesson.Id, lessonToUpdate) ? lesson : null;
        }
    }
}
