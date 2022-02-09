using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.Models.Forms
{
    public class LoginForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage ="L'adresse email est requise!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe est requis!")]
        public string Password { get; set; }
    }
}
