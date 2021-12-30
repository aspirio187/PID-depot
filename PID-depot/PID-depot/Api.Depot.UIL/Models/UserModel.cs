using System;
using System.Collections.Generic;

namespace Api.Depot.UIL.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string RegistrationNumber { get; set; }
        public IEnumerable<RoleModel> Roles { get; set; }
    }
}
