using Api.Depot.UIL.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.Models.Forms
{
    public class LessonTimetableForm
    {
        [Required]
        public DateTime StartsAt { get; set; }

        [Required]
        [DateTimeComparison("StartsAt")]
        public DateTime EndsAt { get; set; }
    }
}
