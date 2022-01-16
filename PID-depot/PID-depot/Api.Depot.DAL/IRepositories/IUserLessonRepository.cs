using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.IRepositories
{
    public interface IUserLessonRepository : IRepositoryBase<int, UserLessonEntity>
    {
        UserLessonEntity GetByUserKey(Guid id);
        UserLessonEntity GetByRoleKey(Guid id);
        UserLessonEntity GetByLessonKey(int id);
    }
}
