using Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.IRepositories
{
    public interface IRoleRepository : IRepositoryBase<Guid, RoleEntity>
    {
        IEnumerable<RoleEntity> GetUserRoles(Guid userId);
        RoleEntity GetRole(string roleName);
    }
}
