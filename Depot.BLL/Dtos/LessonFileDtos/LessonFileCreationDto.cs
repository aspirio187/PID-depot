using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.BLL.Dtos.LessonFileDtos
{
    public class LessonFileCreationDto
    {
        public string FilePath { get; set; }
        public int LessonDetailId { get; set; }
    }
}
