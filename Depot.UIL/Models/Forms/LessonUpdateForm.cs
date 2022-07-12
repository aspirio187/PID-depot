using System;
using System.ComponentModel.DataAnnotations;

namespace Depot.UIL.Models.Forms
{
    public class LessonUpdateForm
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="La description du cours est requise!")]
        [MaxLength(1500, ErrorMessage = "La description doit faire au plus 1500 caractères!")]
        public string Description { get; set; }

        public DateTime EndsAt { get; set; }
    }
}
