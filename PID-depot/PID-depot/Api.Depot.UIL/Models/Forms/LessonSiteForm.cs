using Api.Depot.UIL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.Models.Forms
{
    public class LessonSiteForm
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public DateTime StartsAt { get; set; } = DateTime.Now;

        [Required]
        [DateTimeComparison(nameof(StartsAt))]
        public DateTime EndsAt { get; set; } = DateTime.Now;

        [Required]
        public Guid UserId { get; set; }
    }
}
