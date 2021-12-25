using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.UserDtos
{
    public class UserCreationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }

        public UserEntity MapDAL()
        {
            return new UserEntity()
            {
                Email = Email,
                Password = Password,
                Firstname = Firstname,
                Lastname = Lastname,
                Birthdate = Birthdate,
                RegistrationNumber = "u" + new Random().Next(1, 1000000000).ToString("D6")
        };
    }
}
}
