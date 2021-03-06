using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.IRepositories
{
    public interface ILessonRepository : IRepositoryBase<int, LessonEntity>
    {
        bool AddLessonUser(int lessonId, Guid userId);
        IEnumerable<LessonEntity> GetUserLessons(Guid userId);
        bool DeleteLessonUser(int lessonId, Guid userId);
    }
}
