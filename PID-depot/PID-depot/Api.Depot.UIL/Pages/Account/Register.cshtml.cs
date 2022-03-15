using Api.Depot.UIL.Managers;
using Api.Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace Api.Depot.UIL.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IAuthManager _authManager;

        public RegisterModel(ILogger logger, IAuthManager authManager)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _authManager = authManager ??
                throw new ArgumentNullException(nameof(authManager));
        }

        [BindProperty]
        public RegisterForm RegForm { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; } = false;

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUl = null)
        {
            if (returnUl is not null) ReturnUrl = returnUl;
        }
    }
}
