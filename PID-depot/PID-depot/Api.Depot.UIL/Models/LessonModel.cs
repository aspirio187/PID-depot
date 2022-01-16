using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.Models
{
    public class LessonModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string TeacherName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string TeacherRegistrationNumber { get; set; }
    }
}
