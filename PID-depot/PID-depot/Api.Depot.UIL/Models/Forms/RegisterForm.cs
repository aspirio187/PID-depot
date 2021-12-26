using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.Models.Forms
{
    public class RegisterForm
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_+]).{8,20}$")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
    }
}
