using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models.Forms;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Security.Claims;

namespace Api.Depot.UIL.Pages.Account
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PasswordModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        [BindProperty]
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

            ModelState.AddModelError("Update", "Votre mot de passe actuel est incorrect, veuillez réessayer!");

            return Page();
        }
    }
}
