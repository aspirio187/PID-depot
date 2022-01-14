using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.DAL.Entities;
using Api.Depot.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ??
                throw new ArgumentNullException(nameof(roleRepository));
        }

        public RoleDto CreateRole(RoleCreationDto role)
        {
            if (role is null) throw new ArgumentNullException(nameof(role));

            RoleEntity roleToCreate = role.MapToDAL();
            Guid createRoleId = _roleRepository.Create(roleToCreate);
            return _roleRepository.GetById(createRoleId).MapFromDAL();
        }

        public bool DeleteRole(Guid id)
        {
            return _roleRepository.Delete(id);
        }

        public RoleDto GetRole(Guid id)
        {
            return _roleRepository.GetById(id).MapFromDAL();
        }

        public RoleDto GetRole(string roleName)
        {
            if (roleName is null) throw new ArgumentNullException(nameof(roleName));
            if (roleName.Length == 0) throw new ArgumentException($"{nameof(roleName)} cannot be empty string!");

            return _roleRepository.GetRole(roleName).MapFromDAL();
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            return _roleRepository.GetAll().Select(r => r.MapFromDAL());
        }

        public IEnumerable<RoleDto> GetUserRoles(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException($"{nameof(userId)} can not be empty!");
            return _roleRepository.GetUserRoles(userId).Select(r => r.MapFromDAL());
        }

        public bool RoleExist(string roleName)
        {
            if (roleName is null) throw new ArgumentNullException(nameof(roleName));
            if (roleName.Length == 0) throw new ArgumentException($"{nameof(roleName)} cannot be empty string!");

            return _roleRepository.GetRole(roleName) is not null;
        }

        public RoleDto UpdateRole(RoleDto role)
        {
            if (role is null) throw new ArgumentNullException(nameof(role));

            return _roleRepository.Update(role.Id, role.MapToDAL()) ? role : null;
        }
    }
}
