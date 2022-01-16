using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.Models.Forms
{
    public class LessonForm
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
