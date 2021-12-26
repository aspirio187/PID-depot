using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.LessonDtos
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public LessonDto()
        {

        }

        public LessonDto(LessonEntity lesson)
        {
            Id = lesson.Id;
            Name = lesson.Name;
            Description = lesson.Description;
        }

        public LessonEntity MapDAL()
        {
            return new LessonEntity()
            {
                Id = Id,
                Name = Name,
                Description = Description
            };
        }
    }
}
