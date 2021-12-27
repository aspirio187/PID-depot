using Api.Depot.BLL.IServices;
using Api.Depot.UIL.Models;
using Api.Depot.UIL.Models.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Depot.UIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
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

                // TODO : Envoyer un mail d'activation du compte

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

        }
    }
}
