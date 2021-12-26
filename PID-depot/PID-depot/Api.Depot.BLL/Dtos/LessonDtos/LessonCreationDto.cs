using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.LessonDtos
{
    public class LessonCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public LessonEntity MapDAL()
        {
            return new LessonEntity()
            {
                Name = Name,
                Description = Description,
            };
        }
    }
}
