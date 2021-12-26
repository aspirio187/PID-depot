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

        public LessonDetailDto CreateLessonDetail(LessonDetailCreationDto lessonDetail)
        {
            if (lessonDetail is null) throw new ArgumentNullException(nameof(lessonDetail));

            LessonDetailEntity detailToCreate = lessonDetail.MapDAL();
            int detailCreatedId = _lessonDetailRepository.Create(detailToCreate);
            return new LessonDetailDto(_lessonDetailRepository.GetById(detailCreatedId));

        }

        public bool DeleteLessonDetail(int id)
        {
            return _lessonDetailRepository.Delete(id);
        }

        public IEnumerable<LessonDetailDto> GetDetails()
        {
            return _lessonDetailRepository.GetAll().Select(ld => new LessonDetailDto(ld));
        }

        public LessonDetailDto GetLessonDetail(int lessonTimetableId)
        {
            return new LessonDetailDto(_lessonDetailRepository.GetLessonTimetableDetails(lessonTimetableId)
                .FirstOrDefault());
        }

        public IEnumerable<LessonDetailDto> GetLessonDetails(int lessonId)
        {
            return _lessonDetailRepository.GetLessonDetails(lessonId).Select(ld => new LessonDetailDto(ld));
        }

        public LessonDetailDto UpdateLessonDetail(LessonDetailDto lessonDetail)
        {
            if (lessonDetail is null) throw new ArgumentNullException(nameof(lessonDetail));

            LessonDetailEntity detailToUpdate = lessonDetail.MapDAL();
            return _lessonDetailRepository.Update(lessonDetail.Id, detailToUpdate) ? lessonDetail : null;
        }
    }
}
