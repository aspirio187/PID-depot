using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Entities
{
    public class LessonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public LessonEntity()
        {

        }

        public LessonEntity(IDataRecord data)
        {
            Id = (int)data["id"];
            Name = (string)data["name"];
            Description = (string)data["description"];
        }
    }
}
