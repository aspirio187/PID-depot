using Api.Depot.UIL.Managers;
using Api.Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Depot.UIL.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IAuthManager _authManager;

        public LoginModel(ILogger<LoginModel> logger, IAuthManager authManager)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _authManager = authManager ??
                throw new ArgumentNullException(nameof(authManager));
        }

        [BindProperty]
        public LoginForm Login { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUl = null)
        {
            if (returnUl is not null) ReturnUrl = returnUl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _authManager.LogInAsync(Login.Email, Login.Password, false))
                    {
                        if (string.IsNullOrEmpty(ReturnUrl))
                        {
                            return RedirectToPage("/Index");
                        }
                        else
                        {
                            return Redirect($"~/{ReturnUrl}");
                        }
                    }
                    else
                    {
                        return Page();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return Page();
                }
            }

            return Page();
        }
    }
}
