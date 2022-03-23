using System;

namespace Api.Depot.UIL.Models.Forms
{
    public class LessonUpdateForm
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime EndsAt { get; set; }
    }
}
