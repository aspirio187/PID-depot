using Depot.UIL.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Depot.UIL.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IAuthManager _authManager;

        public LogoutModel(ILogger<LogoutModel> logger, IAuthManager authManager)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _authManager = authManager ??
                throw new ArgumentNullException(nameof(authManager));
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            try
            {
                await _authManager.LogOutAsync();
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToPage("/Index");
                else
                    return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Page();
            }
        }
    }
}
