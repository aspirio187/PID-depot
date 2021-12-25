using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Entities
{
    public class UserRoleEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public UserRoleEntity()
        {

        }

        public UserRoleEntity(IDataRecord data)
        {
            UserId = (Guid)data["user_id"];
            RoleId = (Guid)data["role_id"];
        }
    }
}
