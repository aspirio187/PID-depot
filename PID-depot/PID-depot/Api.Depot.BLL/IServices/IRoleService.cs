using Api.Depot.BLL.Dtos.RoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.IServices
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetRoles();
        IEnumerable<RoleDto> GetUserRoles(Guid userId);
        RoleDto GetRole(Guid id);
        bool DeleteRole(Guid id);
        RoleDto CreateRole(RoleCreationDto role);
        RoleDto UpdateRole(RoleDto role);
    }
}
