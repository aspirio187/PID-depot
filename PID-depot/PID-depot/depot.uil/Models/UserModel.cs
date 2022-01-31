using System;
using System.Collections.Generic;

namespace Depot.UIL.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<RoleModel> Roles { get; set; }
    }
}
