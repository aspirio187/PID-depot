using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Entities
{
    public class LessonFileEntity
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int LessonDetailId { get; set; }

        public LessonFileEntity()
        {

        }

        public LessonFileEntity(IDataRecord data)
        {
            Id = (int)data["id"];
            FilePath = (string)data["file_path"];
            LessonDetailId = (int)data["lesson_detail_id"];
        }
    }
}
