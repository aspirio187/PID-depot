using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string RegistrationNumber { get; set; }

        public UserDto(UserEntity userEntity)
        {
            Id = userEntity.Id;
            Email = userEntity.Email;
            Firstname = userEntity.Firstname;
            Lastname = userEntity.Lastname;
            Birthdate = userEntity.Birthdate;
            RegistrationNumber = userEntity.RegistrationNumber;
        }
    }
}
