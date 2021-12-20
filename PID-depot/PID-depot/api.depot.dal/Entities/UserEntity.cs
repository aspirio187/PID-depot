using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.depot.dal.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
