using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.RoleDtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public RoleDto()
        {

        }

        public RoleDto(RoleEntity role)
        {
            Id = role.Id;
            Name = role.Name;
        }

        public RoleEntity MapDAL()
        {
            return new RoleEntity()
            {
                Id = Id,
                Name = Name,
            };
        }
    }
}
