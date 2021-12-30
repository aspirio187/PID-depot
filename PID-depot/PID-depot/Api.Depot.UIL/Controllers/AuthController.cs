using Api.Depot.BLL.Dtos.UserTokenDtos;
using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Managers;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
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

        [HttpPost("{id}")]
        public IActionResult VerifyToken(Guid userId, [FromBody] string userToken)
        {
            if (userId == Guid.Empty) return BadRequest(userId);
            if (string.IsNullOrEmpty(userToken)) return BadRequest(userToken);

            if (!_userTokenService.TokenIsValid(userId, userToken)) return NotFound(userToken);



            return Ok();
        }
    }
}
