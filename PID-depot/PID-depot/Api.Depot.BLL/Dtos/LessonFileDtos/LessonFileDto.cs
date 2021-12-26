using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.LessonFileDtos
{
    public class LessonFileDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int LessonDetailId { get; set; }

        public LessonFileDto()
        {

        }

        public LessonFileDto(LessonFileEntity lessonFile)
        {
            Id = lessonFile.Id;
            FilePath = lessonFile.FilePath;
            LessonDetailId = lessonFile.LessonDetailId;
        }

        public LessonFileEntity MapDAL()
        {
            return new LessonFileEntity()
            {
                Id = Id,
                FilePath = FilePath,
                LessonDetailId = LessonDetailId,
            };
        }
    }
}
