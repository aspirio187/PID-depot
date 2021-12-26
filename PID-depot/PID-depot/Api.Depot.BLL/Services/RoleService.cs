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

            RoleEntity roleToCreate = role.MapDAL();
            Guid createRoleId = _roleRepository.Create(roleToCreate);
            return new RoleDto(_roleRepository.GetById(createRoleId));
        }

        public bool DeleteRole(Guid id)
        {
            return _roleRepository.Delete(id);
        }

        public RoleDto GetRole(Guid id)
        {
            return new RoleDto(_roleRepository.GetById(id));
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            return _roleRepository.GetAll().Select(r => new RoleDto(r));
        }

        public RoleDto UpdateRole(RoleDto role)
        {
            if(role is null) throw new ArgumentNullException(nameof(role));

            return _roleRepository.Update(role.Id, role.MapDAL()) ? role : null;
        }
    }
}
