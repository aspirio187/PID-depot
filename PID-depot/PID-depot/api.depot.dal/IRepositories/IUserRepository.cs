using api.depot.dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.depot.dal.IRepositories
{
    public interface IUserRepository : IRepositoryBase<Guid, UserEntity>
    {

    }
}
