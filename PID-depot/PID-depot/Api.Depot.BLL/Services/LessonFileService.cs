using Api.Depot.BLL.Dtos.LessonFileDtos;
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
    public class LessonFileService : ILessonFileService
    {
        private readonly ILessonFileRepository _lessonFileRepository;

        public LessonFileDto CreateLessonFile(LessonFileCreationDto lessonFile)
        {
            if (lessonFile is null) throw new ArgumentNullException(nameof(lessonFile));

            LessonFileEntity fileToCreate = lessonFile;
            int fileCreatedId = _lessonFileRepository.Create(fileToCreate);
            return _lessonFileRepository.GetById(fileCreatedId);
        }

        public bool DeleteLessonFile(int id)
        {
            return _lessonFileRepository.Delete(id);
        }

        public LessonFileDto GetFile(int id)
        {
            return _lessonFileRepository.GetById(id);
        }

        public IEnumerable<LessonFileDto> GetFiles()
        {
            return _lessonFileRepository.GetAll().Select(lf => new LessonFileDto(lf));
        }

        public IEnumerable<LessonFileDto> GetLessonDetailFiles(int lessonDetailId)
        {
            return _lessonFileRepository.GetLessonDetailFiles(lessonDetailId).Select(lf => new LessonFileDto(lf));
        }

        public IEnumerable<LessonFileDto> GetLessonFiles(int lessonId)
        {
            return _lessonFileRepository.GetLessonFiles(lessonId).Select(lf => new LessonFileDto(lf));
        }

        public LessonFileDto UpdateLessonFile(LessonFileDto lessonFile)
        {
            if (lessonFile is null) throw new ArgumentNullException(nameof(lessonFile));

            LessonFileEntity fileToUpdate = lessonFile;
            return _lessonFileRepository.Update(lessonFile.Id, fileToUpdate) ? lessonFile : null;
        }
    }
}
