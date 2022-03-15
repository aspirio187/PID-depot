using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.Dtos.UserTokenDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Managers;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Depot.UIL.Pages.Account
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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                if (_userService.EmailExist(RegForm.Email)) return BadRequest(RegForm.Email);
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
                    return BadRequest();
                }

                UserTokenDto userToken = _userTokenService.CreateUserToken(new UserTokenCreationDto()
                {
                    TokenType = UserTokenType.EmailConfirmation,
                    UserId = createdUser.Id
                });

                if (userToken is null) return BadRequest(RegForm);

                if (!_authManager.SendVerificationEmail(createdUser.Email, createdUser.Id, userToken.Token)) return BadRequest(RegForm);

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
