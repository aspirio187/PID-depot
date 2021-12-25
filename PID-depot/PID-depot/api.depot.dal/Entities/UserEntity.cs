using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Entities
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

        public UserEntity()
        {

        }

        public UserEntity(IDataRecord data)
        {

            Id = (Guid)data["id"];
            Email = (string)data["email"];
            NormalizedEmail = (string)data["normalized_email"];
            Password = (string)data["password"];
            Firstname = (string)data["firstname"];
            Lastname = (string)data["lastname"];
            Birthdate = (DateTime)data["birth_date"];
            RegistrationNumber = (string)data["registration_number"];
        }
    }
}
