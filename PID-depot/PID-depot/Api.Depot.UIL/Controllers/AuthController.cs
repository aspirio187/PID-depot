using Api.Depot.BLL.Dtos.RoleDtos;
using Api.Depot.BLL.Dtos.UserTokenDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Managers;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
using Api.Depot.UIL.Static_Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace Api.Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserTokenService _userTokenService;
        private readonly IAuthManager _authManager;

        public AuthController(IUserService userService, IAuthManager authManager, IRoleService roleService, IUserTokenService userTokenService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _authManager = authManager ??
                throw new ArgumentNullException(nameof(authManager));
            _roleService = roleService ??
                throw new ArgumentNullException(nameof(roleService));
            _userTokenService = userTokenService ??
                throw new ArgumentNullException(nameof(userTokenService));
        }

        [HttpPost]
        [Route(nameof(Register))]
        public IActionResult Register([FromBody] RegisterForm register)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                if (_userService.EmailExist(register.Email)) return BadRequest(register.Email);
                UserModel createdUser = _userService.CreateUser(register.MapToBLL()).MapFromBLL();
                if (createdUser is null) return BadRequest(register);

                if (!_roleService.RoleExist(RolesData.USER_ROLE))
                {
                    _roleService.CreateRole(new RoleCreationDto()
                    {
                        Name = RolesData.USER_ROLE
                    });
                }

                UserTokenDto userToken = _userTokenService.CreateUserToken(new UserTokenCreationDto()
                {
                    TokenType = UserTokenType.EmailConfirmation,
                    UserId = createdUser.Id
                });

                if (userToken is null) return BadRequest(register);

                if (!_authManager.SendVerificationEmail(createdUser.Email, createdUser.Id, userToken.Token)) return BadRequest(register);

                return Ok(createdUser);
            }
            catch (Exception e)
            {
#if DEBUG
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
#else 
                return BadRequest();
#endif
            }
        }

        [HttpPost]
        [Route(nameof(Login))]
        public IActionResult Login([FromBody] LoginForm login)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                UserModel loggedInUser = _userService.UserLogin(login.Email, login.Password).MapFromBLL();
                if (loggedInUser is null) return BadRequest(login);
                if (!_userService.AccountIsActive(loggedInUser.Id)) return BadRequest("Account is not activated");

                loggedInUser.Roles = _roleService.GetUserRoles(loggedInUser.Id).Select(ur => ur.MapFromBLL());
                string generatedToken = _authManager.GenerateJwtToken(loggedInUser);

                return Ok(generatedToken);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return BadRequest(login);
            }
        }

        [HttpGet("/activation")]
        public IActionResult Verify(Guid id, string token)
        {
            if (id == Guid.Empty) return BadRequest(id);
            if (string.IsNullOrEmpty(token)) return BadRequest(token);

            if (!_userTokenService.TokenIsValid(id, token)) return NotFound(token);

            if (!_userService.ActivateAccount(id)) return BadRequest(id);

            return Ok();
        }
    }
}
