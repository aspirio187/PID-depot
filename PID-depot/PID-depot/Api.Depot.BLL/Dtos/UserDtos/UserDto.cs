using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string RegistrationNumber { get; set; }
        public Guid SecurityStamp { get; set; }
        public Guid ConcurrencyStamp { get; set; }
        public bool IsActivated { get; set; }
    }
}
