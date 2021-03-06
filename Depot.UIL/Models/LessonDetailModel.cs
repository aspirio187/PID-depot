using System.Collections.Generic;

namespace Depot.UIL.Models
{
    public class LessonDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int LessonTimetableId { get; set; }
        public IEnumerable<LessonFileModel> LessonFiles { get; set; }
    }
}
