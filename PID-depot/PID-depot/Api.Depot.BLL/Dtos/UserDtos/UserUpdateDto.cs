using Api.Depot.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL.Dtos.UserDtos
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }

        public UserUpdateDto()
        {
        }

        public UserEntity MapDAL()
        {
            return new UserEntity()
            {
                Id = Id,
                Firstname = Firstname,
                Lastname = Lastname,
                Birthdate = Birthdate,
            };
        }
    }
}
