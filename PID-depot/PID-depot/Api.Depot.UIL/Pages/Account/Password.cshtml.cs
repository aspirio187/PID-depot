using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace Api.Depot.UIL.Pages.Account
{
    public class PasswordModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public PasswordForm Password { get; set; }

        public PasswordModel(ILogger<PasswordModel> logger, IUserService userService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _userService = userService;
        }

        public void OnGet()
        {
            Password = new PasswordForm()
            {
                Id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            Guid userID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (Password.Id != userID)
            {
                _logger.LogError("The ID : {0} doesn't match with this user ID : {1}", Password.Id, userID);
                return Page();
            }

            if (_userService.UpdatePassword(Password.Id, Password.OldPassword, Password.NewPassword))
            {
                ViewData["Success"] = "Votre mot de passe a bien été modifié!";
                return Page();
            }

            ModelState.AddModelError("Update", "Une erreur est survenue lors de la mise à jours, veuillez " +
                "réessayer!");

            return Page();
        }
    }
}
