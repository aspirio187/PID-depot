using Api.Depot.BLL.Dtos.LessonDetailDtos;
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
    public class LessonDetailService : ILessonDetailService
    {
        private readonly ILessonDetailRepository _lessonDetailRepository;

        public LessonDetailService(ILessonDetailRepository lessonDetailRepository)
        {
            _lessonDetailRepository = lessonDetailRepository ??
                throw new ArgumentNullException(nameof(lessonDetailRepository));
        }

        public LessonDetailDto CreateLessonDetail(LessonDetailCreationDto lessonDetail)
        {
            if (lessonDetail is null) throw new ArgumentNullException(nameof(lessonDetail));

            LessonDetailEntity detailToCreate = lessonDetail.MapToDAL();
            int detailCreatedId = _lessonDetailRepository.Create(detailToCreate);
            return _lessonDetailRepository.GetById(detailCreatedId).MapFromDAL();

        }

        public bool DeleteLessonDetail(int id)
        {
            return _lessonDetailRepository.Delete(id);
        }

        public LessonDetailDto GetDetail(int id)
        {
            if (id == 0) throw new ArgumentException($"{nameof(id)} cannot be 0!");
            LessonDetailDto detailFromRepo = _lessonDetailRepository.GetById(id).MapFromDAL();
            return detailFromRepo;
        }

        public IEnumerable<LessonDetailDto> GetDetails()
        {
            return _lessonDetailRepository.GetAll().Select(ld => ld.MapFromDAL());
        }

        public LessonDetailDto GetLessonDetail(int lessonTimetableId)
        {
            return _lessonDetailRepository.GetLessonTimetableDetails(lessonTimetableId).FirstOrDefault().MapFromDAL();
        }

        public IEnumerable<LessonDetailDto> GetLessonDetails(int lessonId)
        {
            return _lessonDetailRepository.GetLessonDetails(lessonId).Select(ld => ld.MapFromDAL());
        }

        public LessonDetailDto UpdateLessonDetail(LessonDetailDto lessonDetail)
        {
            if (lessonDetail is null) throw new ArgumentNullException(nameof(lessonDetail));

            LessonDetailEntity detailToUpdate = lessonDetail.MapToDAL();
            return _lessonDetailRepository.Update(lessonDetail.Id, detailToUpdate) ? lessonDetail : null;
        }
    }
}
