using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.IRepositories
{
    public interface ILessonTimetableRepository : IRepositoryBase<int, LessonTimetableEntity>
    {
        IEnumerable<LessonTimetableEntity> GetLessonTimetables(int lessonId);
    }
}
