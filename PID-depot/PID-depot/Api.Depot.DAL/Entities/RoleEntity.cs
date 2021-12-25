using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public RoleEntity()
        {

        }

        public RoleEntity(IDataRecord data)
        {
            Id = (Guid)data["id"];
            Name = (string)data["name"];
        }
    }
}
