using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.IRepositories
{
    public interface ILessonDetailRepository : IRepositoryBase<int, LessonDetailEntity>
    {
        IEnumerable<LessonDetailEntity> GetLessonTimetableDetails(int lessonTimetableId);
        IEnumerable<LessonDetailEntity> GetLessonDetails(int lessonId);
    }
}
