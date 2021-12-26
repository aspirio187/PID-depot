using Api.Depot.BLL.Dtos.LessonFileDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.IServices
{
    public interface ILessonFileService
    {
        IEnumerable<LessonFileDto> GetFiles();
        IEnumerable<LessonFileDto> GetLessonFiles(int lessonId);
        IEnumerable<LessonFileDto> GetLessonDetailFiles(int lessonDetailId);
        LessonFileDto GetFile(int id);
        bool DeleteLessonFile(int id);
        LessonFileDto CreateLessonFile(LessonFileCreationDto lessonFile);
        LessonFileDto UpdateLessonFile(LessonFileDto lessonFile);
    }
}
