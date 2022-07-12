using System.ComponentModel.DataAnnotations;

namespace Depot.UIL.Models.Forms
{
    public class LoginSiteForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "L'adresse email est requise!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe est requis!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
