using Api.Depot.BLL.Dtos.UserDtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.Models.Forms
{
    public class UserForm
    {
        [Required(AllowEmptyStrings = false)]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(75)]
        public string Lastname { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public UserUpdateDto MapBLL()
        {
            return new UserUpdateDto()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                Id = Id,
                Lastname = Lastname
            };
        }
    }
}
