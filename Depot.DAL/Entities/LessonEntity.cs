using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.Entities
{
    public class LessonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public LessonEntity()
        {

        }
    }
}
