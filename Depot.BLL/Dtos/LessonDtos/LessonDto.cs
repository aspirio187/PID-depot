using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.BLL.Dtos.LessonDtos
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public LessonDto()
        {

        }
    }
}
