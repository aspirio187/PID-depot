using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.LessonFileDtos
{
    public class LessonFileCreationDto
    {
        public string FilePath { get; set; }
        public int LessonDetailId { get; set; }

        public static implicit operator LessonFileEntity(LessonFileCreationDto lessonFile)
        {
            return new LessonFileEntity()
            {
                FilePath = lessonFile.FilePath,
                LessonDetailId = lessonFile.LessonDetailId,
            };
        }
    }
}
