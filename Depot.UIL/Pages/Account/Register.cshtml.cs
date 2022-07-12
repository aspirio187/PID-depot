using Depot.BLL.Dtos.RoleDtos;
using Depot.BLL.Dtos.UserTokenDtos;
using Depot.BLL.IServices;
using Depot.UIL.Managers;
using Depot.UIL.Models;
using Depot.UIL.Models.Forms;
using Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Depot.UIL.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IAuthManager _authManager;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserTokenService _userTokenService;

        public RegisterModel(ILogger<RegisterModel> logger, IAuthManager authManager, IUserService userService, IRoleService roleService, IUserTokenService userTokenService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _authManager = authManager ??
                throw new ArgumentNullException(nameof(authManager));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ??
                throw new ArgumentNullException(nameof(roleService));
            _userTokenService = userTokenService ??
                throw new ArgumentNullException(nameof(userTokenService));
        }

        [BindProperty]
        public RegisterForm RegForm { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUl = null)
        {
            if (returnUl is not null) ReturnUrl = returnUl;
        }

        public IActionResult OnPostAsync()
        {
           
            if (!ModelState.IsValid) return Page();
            try
            {
                if (_userService.EmailExist(RegForm.Email))
                {
                    ModelState.AddModelError("email exist", "L'adresse email est indisponible!");
                    return Page();
                }

                UserModel createdUser = _userService.CreateUser(RegForm.MapToBLL()).MapFromBLL();
                if (createdUser is null)
                {
                    ModelState.AddModelError("User creation", "Une erreur est survenue lors de la création de l'utilisateur");
                    return Page();
                }

                if (!_roleService.RoleExist(RolesData.USER_ROLE))
                {
                    _roleService.CreateRole(new RoleCreationDto()
                    {
                        Name = RolesData.USER_ROLE
                    });
                }

                if (!_userService.AddUserRole(createdUser.Id, _roleService.GetRole(RolesData.USER_ROLE).Id))
                {
                    ModelState.AddModelError("Role add", "L'ajout du rôle a échoué!");
                    return Page();
                }

                UserTokenDto userToken = _userTokenService.CreateUserToken(new UserTokenCreationDto()
                {
                    TokenType = UserTokenType.EmailConfirmation,
                    UserId = createdUser.Id
                });

                if (userToken is null)
                {
                    ModelState.AddModelError("User token", "Impossible de créer un token d'activation");
                    return Page();
                }

                if (!_authManager.SendVerificationEmail(createdUser.Email, createdUser.Id, userToken.Token))
                {
                    ModelState.AddModelError("Verification email", "Le mail de vérification n'a pas pu être envoyé");
                    return Page();
                }

                ViewData["Inscription"] = "Votre compte a bien été crée, un mail d'activation vous a été envoyé";
                return Page();
            }
            catch (Exception e)
            {
#if DEBUG
                _logger.LogError(e.Message);
                return Page();
#else 
                return Page();
#endif
            }
        }
    }
}
