using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.DAL.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public RoleEntity()
        {

        }
    }
}
