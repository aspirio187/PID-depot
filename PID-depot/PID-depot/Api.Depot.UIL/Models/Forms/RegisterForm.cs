using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Depot.UIL.Models.Forms
{
    public class RegisterForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "L'adresse email est requise!")]
        [EmailAddress(ErrorMessage = "Le format de l'adresse email est incorrect!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez choisir un mot de passe!")]
        [RegularExpression("(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_+]).{8,20}$",
            ErrorMessage = "Le mot de passe doit faire entre 8 et 20 caractères et doit contenir au moins une minuscule, une majuscule, un nombre et un caractère spécial!")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez confirmer le mot de passe!")]
        [Compare("Password", ErrorMessage = "Les mots de passes ne concordent pas!")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez entrer votre prénom!")]
        [MaxLength(50, ErrorMessage = "Le prénom ne peut pas faire plus de 50 caractères!")]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez entrer votre nom de famille!")]
        [MaxLength(100, ErrorMessage = "Le nom de famille ne peut pas faire plus de 100 caractères!")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Veuillez entrer votre date de naissance!")]
        public DateTime Birthdate { get; set; }
    }
}
