using Depot.BLL.IServices;
using Depot.UIL.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace Depot.UIL.Pages.Account
{
    public class ActivationModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserService _userService;

        public ActivationModel(ILogger<ActivationModel> logger, IUserTokenService userTokenService, IUserService userService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _userTokenService = userTokenService ??
                throw new ArgumentNullException(nameof(userTokenService));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        public void OnGet(Guid id, string token)
        {
            if (string.IsNullOrEmpty(token)) RedirectToPage("/Index");
            if (id == Guid.Empty) RedirectToPage("/Index");

            if (!_userTokenService.TokenIsValid(id, token))
            {
                ViewData["ActivationMessage"] = "Le lien d'activation a expiré !";
            }
            else
            {
                if (!_userService.ActivateAccount(id))
                {
                    ViewData["ActivationMessage"] = "L'activation a échouée !";
                }
                else
                {
                    ViewData["ActivationMessage"] = "Votre compte a bien été activé !";
                }
            }

        }
    }
}
