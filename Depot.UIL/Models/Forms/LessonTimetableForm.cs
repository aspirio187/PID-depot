using Depot.UIL.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Depot.UIL.Models.Forms
{
    public class LessonTimetableForm
    {
        [Required(ErrorMessage = "La date de début est requise!")]
        public DateTime StartsAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La date de fin est requise!")]
        [DateTimeComparison("StartsAt", ErrorMessage = "La date de fin ne peut pas précéder ou égaler la date de début!")]
        public DateTime EndsAt { get; set; } = DateTime.Now;

        public int LessonId { get; set; }
    }
}
