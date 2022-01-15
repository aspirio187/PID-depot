using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.Models.Forms
{
    public class PasswordForm
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }

        [RegularExpression("(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_+]).{8,20}$")]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword))]
        public string NewPasswordConfirm { get; set; }
    }
}
