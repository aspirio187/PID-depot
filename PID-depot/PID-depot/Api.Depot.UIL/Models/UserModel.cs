using Api.Depot.BLL.Dtos.UserDtos;
using System;

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

        public UserModel(UserDto userDto)
        {
            Id = (Guid)userDto?.Id;
            Email = userDto?.Email;
            Firstname = userDto?.Firstname;
            Lastname = userDto?.Lastname;
            Birthdate = (DateTime)userDto?.Birthdate;
            RegistrationNumber = userDto?.RegistrationNumber;
        }
    }
}
