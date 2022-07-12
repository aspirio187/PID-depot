using Depot.BLL.Dtos.UserDtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Depot.UIL.Models.Forms
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
    }
}
