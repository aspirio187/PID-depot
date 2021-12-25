using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.IRepositories
{
    public interface ILessonTimetableRepository : IRepositoryBase<int, LessonTimetableEntity>
    {
    }
}
