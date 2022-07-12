using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.BLL.Dtos.LessonFileDtos
{
    public class LessonFileDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int LessonDetailId { get; set; }
    }
}
